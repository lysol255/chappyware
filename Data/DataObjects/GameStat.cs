using System.Collections.Generic;

namespace Chappyware.Data.DataObjects
{
    public class GameStat
    {
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
            if(HomeTeamCode == teamCode)
            {
                playerStat = HomeTeamPlayerStats[playerName];
            }
            else
            {
                playerStat = AwayTeamPlayerStats[playerName];
            }

            return playerStat;
        }

    }
}