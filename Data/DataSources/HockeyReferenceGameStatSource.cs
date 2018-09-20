using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using Chappyware.Data.DataObjects;
using System.Threading;

namespace Chappyware.Data.DataSources
{
    public class HockeyReferenceGameStatSource
    {
        // Regex expressions
        private const string GameTableRegex = "id=\"div_games\".*?</div>";
        private const string GameURLRegex = "href=\"\\/boxscores\\/.*?html\"";
        private const string AllSkatersDivRegex = "div_.{3}_skaters.*?</table></div>";
        private const string TeamCodeRegex = "div_.{3}_skaters";
        private const string PlayerRowRegex = "<tr ><th scope=\"row\" class=\"right \" data-stat=\"ranker\".*?<\\/tr>";
        private const string StatLineRegex = "<td .*?</td>";

        // game url is the key
        private Dictionary<string, GameStat> _HistoricalStats;

        public HockeyReferenceGameStatSource(Dictionary<string, GameStat> historicalGameStats)
        {
            _HistoricalStats = historicalGameStats;
        }

        // Urls
        private const string BaseURL = "http://www.hockey-reference.com";
        private const string AllGamesUrl = "http://www.hockey-reference.com/leagues/NHL_2018_games.html";

        /// <summary>
        /// Updates the internal historical stats that this data source was constructed with.  Returns
        /// the dictionary with any new entries.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, GameStat> UpdateHistoricalStats()
        {
            // get game urls
            List<string> allGameUrls = GetGameUrls();

            // only process new game Urls
            List<string> newGameUrls = FilterOutAlreadyStoredGames(allGameUrls);

            //List<string> newGameUrls = new List<string> { "https://www.hockey-reference.com/boxscores/201710040WPG.html"};


            foreach (string gameUrl in newGameUrls)
            {
                GameStat gameStat = null;
                
                // has this game already been recorded
                if (_HistoricalStats.ContainsKey(gameUrl))
                {
                    continue;
                }

                // create game stats
                gameStat = GetGameStats(gameUrl);

                // add the game if it processed correctly
                if (gameStat != null)
                {
                    _HistoricalStats.Add(gameUrl, gameStat);
                }
                else
                {
                    //throw new Exce
                }
            }

            return _HistoricalStats;

        }

        private List<string> FilterOutAlreadyStoredGames(List<string> allGameUrls)
        {

            var newGameUrls = allGameUrls.Where(url => !_HistoricalStats.Keys.Any(exitingUrl => exitingUrl.Equals(url)));
            return newGameUrls.ToList();
        }

        private GameStat GetGameStats(string gameUrl)
        {
            GameStat gameStats = new GameStat();
            gameStats.GameUrl = gameUrl;

            // request the game data
            string gamePageHtml = HockeyReferenceRequest.MakeRequest(gameUrl);

            // get both team stat tables
            Regex teamTableSearch = new Regex(AllSkatersDivRegex);
            MatchCollection teamTableColelction = teamTableSearch.Matches(gamePageHtml);

            // get team code (3 letters, eg. CHI) 
            gameStats.HomeTeamCode = GetHomeTeamCode(gameUrl);
            gameStats.AwayTeamCode = GetAwayTeamCode(teamTableColelction, gameStats.HomeTeamCode);
             
            // iterate over each team table
            foreach(Match teamTable in teamTableColelction)
            {
                Regex playerRowRegex = new Regex(PlayerRowRegex);
                MatchCollection playerRows = playerRowRegex.Matches(teamTable.Value);

                string teamCode = GetTeamCode(teamTable.Value);

                foreach(Match playerStat in playerRows)
                {

                    PlayerGameStat playerGameStats = new PlayerGameStat();
                    playerGameStats.TeamCode = teamCode;

                    // read out the stats
                    Regex statLineRegex = new Regex(StatLineRegex);
                    MatchCollection statRows = statLineRegex.Matches(playerStat.Value);

                    foreach(Match statRow in statRows)
                    {
                        // read out the name and value of a stat
                        XDocument statDoc = XDocument.Parse(statRow.Value);
                        string statName = statDoc.Root.Attribute("data-stat").Value;
                        string statValue = statDoc.Root.Value;

                        // handle assigning the stat
                        ProcessStat(statName, statValue, playerGameStats);
                    }

                    AddPlayerGameStat(teamCode, gameStats, playerGameStats);

                }


            }

            // delay
            Rest();

            return gameStats;
        }

        /// <summary>
        /// Just adds the player's stats to the correct team's list of gamestats
        /// </summary>
        /// <param name="teamCode"></param>
        /// <param name="gameStats"></param>
        /// <param name="playerGameStats"></param>
        private void AddPlayerGameStat(string teamCode, GameStat gameStats, PlayerGameStat playerGameStats)
        {
            if(teamCode == gameStats.HomeTeamCode)
            {
                gameStats.HomeTeamPlayerStats.Add(playerGameStats.Name, playerGameStats);
            }
            else
            {
                gameStats.AwayTeamPlayerStats.Add(playerGameStats.Name, playerGameStats);
            }
        }

        private string GetAwayTeamCode(MatchCollection teamTableColelction, string homeTeamCode)
        {
            string awayTeamCode = null;
            foreach (Match teamTable in teamTableColelction)
            {
                string teamCode = GetTeamCode(teamTable.Value);
                if (teamCode != homeTeamCode)
                {
                    awayTeamCode = teamCode;
                }
            }
            return awayTeamCode;
        }


        /// <summary>
        /// Gets the team code by reading the specific div id from the skater table
        /// something like div_.{3}_skaters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetTeamCode(string value)
        {
            Regex teamCodeRegex = new Regex(TeamCodeRegex);
            Match teamCodeMatch = teamCodeRegex.Match(value);

            string teamCode = teamCodeMatch.Value;
            // eg all_STL_skaters
            teamCode = teamCode.Substring(teamCode.Length - 11, teamCode.Length - 12);
            return teamCode;
        }

        /// <summary>
        /// Parses the home team code from the url of the page.  Home team will always be a
        /// suffix of the game stats page.  Eg. 201610120CHI.html
        /// </summary>
        /// <param name="gameUrl"></param>
        /// <returns></returns>
        private string GetHomeTeamCode(string gameUrl)
        {
            // home team code is always the last 3 characters before the .html
            //http://www.hockey-reference.com/boxscores/201610120CHI.html
            string homeTeamCOde = gameUrl.Substring(gameUrl.Length-8, 3);
            return homeTeamCOde;
        }

        private void ProcessStat(string statName, string statValue, PlayerGameStat player)
        {
            switch (statName)
            {
                case "player":
                    player.Name = statValue;
                    break;
                case "goals":
                    player.Goals = ParseNumericStat(statValue);
                    break;
                case "assists":
                    player.Assists = ParseNumericStat(statValue);
                    break;
                case "plus_minus":
                    player.PlusMinus = ParseNumericStat(statValue);
                    break;
                case "pen_min":
                    player.PenaltyMin = ParseNumericStat(statValue);
                    break;
                case "goals_ev":
                    player.EvenGoals = ParseNumericStat(statValue);
                    break;
                case "goals_pp":
                    player.PowerPlayGoals = ParseNumericStat(statValue);
                    break;
                case "goals_sh":
                    player.ShortHandedGoals = ParseNumericStat(statValue);
                    break;
                case "goals_gw":
                    player.GameWinningGoals = ParseNumericStat(statValue);
                    break;
                case "assists_ev":
                    player.EventAssists = ParseNumericStat(statValue);
                    break;
                case "shots":
                    player.Shots = ParseNumericStat(statValue);
                    break;
                case "assists_pp":
                    player.PowerPlayAssists = ParseNumericStat(statValue);
                    break;
                case "assists_sh":
                    player.ShortHandedAssists = ParseNumericStat(statValue);
                    break;
                case "shifts":
                    player.Shifts = ParseNumericStat(statValue);
                    break;
                case "time_on_ice":
                    player.TOI = statValue;
                    break;
            }
        }

        private int ParseNumericStat(string statValue)
        {
            return !string.IsNullOrEmpty(statValue) ? int.Parse(statValue) : 0;
        }

        private List<string> GetGameUrls()
        {
            List<string> gameUrls = new List<string>();

            string allGamesPageHtml = HockeyReferenceRequest.MakeRequest(AllGamesUrl);

            // find the player table
            Regex gameTableSearch = new Regex(GameTableRegex);
            Match dataTable = gameTableSearch.Match(allGamesPageHtml);

            // match each game url row into a collection of game urls
            Regex gameRows = new Regex(GameURLRegex);
            MatchCollection gameCollection = gameRows.Matches(dataTable.Value);

            //get the stats for each game
            foreach(Match gameUrlMatch in gameCollection)
            {
                // clean up the URL
                string gameUrl = gameUrlMatch.Value.Replace("href=\"", "");
                gameUrl = gameUrl.Remove(gameUrl.Length - 1, 1);

                // build the full game link Url
                gameUrl = string.Format("{0}{1}"
                    , BaseURL
                    , gameUrl);

                gameUrls.Add(gameUrl);

            }

            return gameUrls;
        }
        
        private void Rest()
        {
            Thread.Sleep(1000);
        }
    }
}
