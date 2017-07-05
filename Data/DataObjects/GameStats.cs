using System.Collections.Generic;

namespace Chappyware.Data.DataSources
{
    public class GameStats
    {
        public string GameUrl { get; set; }

        public string HomeTeamCode { get; set; }

        public string AwayTeamCode { get; set; }

        public List<PlayerGameStats> AwayTeamPlayerStats { get; set; }

        public List<PlayerGameStats> HomeTeamPlayerStats { get; set; }

        public GameStats()
        {
            AwayTeamPlayerStats = new List<PlayerGameStats>();

            HomeTeamPlayerStats = new List<PlayerGameStats>();
        }

    }
}