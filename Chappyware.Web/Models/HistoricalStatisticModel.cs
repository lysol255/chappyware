using Chappyware.Data;
using System;

namespace Chappyware.Web.Models
{
    public class HistoricalStatisticModel
    {
        public int Assists { get; set; }
        public int Goals { get; set; }
        public string AverageTimeOnIce { get; set; }
        public int GamesPlayed { get; set; }
        public DateTime RecordDate { get; set; }

        public HistoricalStatisticModel(Statistic stat)
        {
            Goals = stat.Goals;
            Assists = stat.Assists;
            AverageTimeOnIce = stat.AvgTOI;
            GamesPlayed = stat.GamesPlayed;
            RecordDate = stat.RecordDate;
        }

    }
}