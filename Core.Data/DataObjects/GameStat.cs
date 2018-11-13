using System;
using System.Collections.Generic;

namespace Core.Data.DataObjects
{
    public class GameStat
    {
        //public const string https://www.hockey-reference.com/boxscores/201710040EDM.html;

        public const string UrlToRemoveHttp = @"http://www.hockey-reference.com/boxscores/";
        public const string UrlToRemoveHttps = @"https://www.hockey-reference.com/boxscores/";


        public string GameUrl { get; set; }

        public string HomeTeamCode { get; set; }

        public string AwayTeamCode { get; set; }

        /// <summary>
        /// Key is the player name
        /// </summary>
        public Dictionary<string,PlayerGameStat> AwayTeamPlayerStats { get; set; }


        /// <summary>
        /// Key is the player name
        /// </summary>
        public Dictionary<string, PlayerGameStat> HomeTeamPlayerStats { get; set; }

        public GameStat()
        {
            AwayTeamPlayerStats = new Dictionary<string, PlayerGameStat>();

            HomeTeamPlayerStats = new Dictionary<string, PlayerGameStat>();
        }

        public PlayerGameStat GetPlayerGameStat(string playerName, string teamCode)
        {
            PlayerGameStat playerStat = null;
            if(HomeTeamCode == teamCode
                &&
                HomeTeamPlayerStats.ContainsKey(playerName))
            {                
                playerStat = HomeTeamPlayerStats[playerName];
            }
            else if(AwayTeamPlayerStats.ContainsKey(playerName))
            {
                playerStat = AwayTeamPlayerStats[playerName];
            }

            return playerStat;
        }

        public DateTime GetGameDate()
        {
            string gameDate = GameUrl.Replace(UrlToRemoveHttp,"");
            gameDate = gameDate.Replace(UrlToRemoveHttps, "");
            gameDate = gameDate.Replace(".html", "");

            string year = gameDate.Substring(0, 4);
            string month = gameDate.Substring(4, 2);
            string day = gameDate.Substring(6, 2);

            DateTime date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            return date;

        }
    }
}