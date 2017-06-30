using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

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



        // Urls
        private const string BaseURL = "http://www.hockey-reference.com";
        

        public List<GameStats> GetLatestGameStats()
        {

            // get game urls
            //List<string> gameUrls = GetGameUrls();

            List<string> gameUrls = new List<string> { "http://www.hockey-reference.com/boxscores/201610120CHI.html" };

            string gameUrl = gameUrls.First();

            // create game stats
            GameStats gameStat = GetGameStats(gameUrl);

            return new List<GameStats>();

        }

        private GameStats GetGameStats(string gameUrl)
        {
            GameStats gameStats = new GameStats();
            
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

                    PlayerGameStats playerGameStats = new PlayerGameStats();

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

        private void AddPlayerGameStat(string teamCode, GameStats gameStats, PlayerGameStats playerGameStats)
        {
            if(teamCode == gameStats.HomeTeamCode)
            {
                gameStats.HomeTeamPlayerStats.Add(playerGameStats);
            }
            else
            {
                gameStats.AwayTeamPlayerStats.Add(playerGameStats);
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

        private string GetTeamCode(string value)
        {
            Regex teamCodeRegex = new Regex(TeamCodeRegex);
            Match teamCodeMatch = teamCodeRegex.Match(value);

            string teamCode = teamCodeMatch.Value;
            // eg all_STL_skaters
            teamCode = teamCode.Substring(teamCode.Length - 11, teamCode.Length - 12);
            return teamCode;
        }

        private string GetHomeTeamCode(string gameUrl)
        {
            // home team code is always the last 3 characters before the .html
            //http://www.hockey-reference.com/boxscores/201610120CHI.html
            string homeTeamCOde = gameUrl.Substring(gameUrl.Length-8, 3);
            return homeTeamCOde;
        }

        private void ProcessStat(string statName, string statValue, PlayerGameStats player)
        {
            switch (statName)
            {
                case "player":
                    player.Name = statValue;
                    break;
                case "goals":
                    player.Goals = !string.IsNullOrEmpty(statValue) ? int.Parse(statValue) : 0;
                    break;
                case "plus_minus":
                    player.PlusMinus = !string.IsNullOrEmpty(statValue) ? int.Parse(statValue) : 0;
                    break;
                case "pen_min":
                    player.PenaltyMin = !string.IsNullOrEmpty(statValue) ? int.Parse(statValue) : 0;
                    break;
                case "time_on_ice":
                    player.TOI = statValue;
                    break;
            }
        }

        private List<string> GetGameUrls()
        {
            List<string> gameUrls = new List<string>();

            string allGamesPageHtml = HockeyReferenceRequest.MakeRequest("http://www.hockey-reference.com/leagues/NHL_2017_games.html");

            // start to parse out each game url

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
            //Thread.Sleep(1000);
        }
    }
}
