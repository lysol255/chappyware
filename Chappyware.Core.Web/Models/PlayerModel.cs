using System.Collections.Generic;
using Chappyware.Data;

namespace Chappyware.Web.Models
{
    public class PlayerModel
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

        public PlayerModel()
        {
            HistoricalStats = new List<HistoricalStatisticModel>();
        }

        public PlayerModel(List<Statistic> stats)
        {
            foreach (Statistic stat in stats)
            {
                HistoricalStats.Add(new HistoricalStatisticModel(stat));
            }
        }
    }
}