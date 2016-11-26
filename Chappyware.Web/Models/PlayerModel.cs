using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chappyware.Web.Models
{
    public class PlayerStatsModel
    {
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Points { get; set; }
        public string Name { get; set; }
        public int GamesPlayed { get; set; }
        public string AverageTimeOnIce { get; set; }
        public double PointsPerGame { get; set; }
        public int DraftRound { get; set; }

        public List<HistoricalStatisticModel> HistoricalStats { get; set; }

        public PlayerStatsModel()
        {
            HistoricalStats = new List<HistoricalStatisticModel>();
        }
    }
}