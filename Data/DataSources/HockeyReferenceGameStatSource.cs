using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Chappyware.Data.DataSources
{
    public class HockeyReferenceGameStatSource
    {
        // Regex expressions
        private const string GameTableRegex = "id=\"div_games\".*?</div>";
        private const string GameURLRegex = "href=\"\\/boxscores\\/.*?html\"";
        private const string AllSkatersDivRegex = "div_.{3}_skaters.*?</table></div>";
        private const string PlayerRowRegex = "<tr ><th scope=\"row\" class=\"right \" data-stat=\"ranker\".*?<\\/tr>";



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

            // iterate over each team table
            foreach(Match teamTable in teamTableColelction)
            {
                Regex playerRowRegex = new Regex(PlayerRowRegex);
                MatchCollection playerRows = playerRowRegex.Matches(teamTable.Value);

                foreach(Match playerStat in playerRows)
                {
                    // read out the stats
                }

            }

            // delay
            Rest();

            return gameStats;
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
