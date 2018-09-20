using Chappyware.Data.Factories;
using System;
using System.Linq;

namespace Chappyware.Data
{
    public class FantasyPlayer
    {
        private string _PlayerName;
        private string _TeamName;

        public FantasyPlayer(Player player)
        {
            _PlayerName = player.Name;
            _TeamName = player.CurrentTeam;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Player {
            get
            {
                return PlayerFactory.Instance.GetPlayer(_PlayerName, _TeamName);
            }

        }

        public DateTime OwnedStartDate { get; set; }

        public DateTime OwnedEndDate { get; set; }

        public int DraftRound { get; set; }

        // Must fix this later and make it relative to draft dates
        public int GamesPlayed
        {
            get
            {
                var games = from gameStat in Player.GameStats.PlayerStats
                            where gameStat.GameDate >= OwnedStartDate
                            &&
                            gameStat.GameDate < OwnedEndDate
                            select gameStat;


                return games.Count();
            }
        }

        public Statistic GetCurrentStats()
        {
            // get the record for the player's owned date
            Statistic firstOwnedStat = GetOwnedStartDateStats();

            // get the most recent stat
            Statistic mostRecentStat = GetMostRecentOwnedStatistic();

            Statistic currentStat = new Statistic();
            currentStat.Goals = mostRecentStat.Goals - firstOwnedStat.Goals;
            currentStat.Assists = mostRecentStat.Assists - firstOwnedStat.Assists;
            currentStat.GamesPlayed = mostRecentStat.GamesPlayed - firstOwnedStat.GamesPlayed;
            currentStat.AvgTOI = mostRecentStat.AvgTOI;

            return currentStat;
        }


        private Statistic GetMostRecentOwnedStatistic()
        {
            Statistic mostRecentStat = null;
            var currentStat = from statistic in Player.Stats
                              where statistic.RecordDate >= OwnedStartDate && statistic.RecordDate < OwnedEndDate
                              select statistic;
            if (currentStat.Count() > 0)
            {
                DateTime mostRecent = currentStat.Select(c => c.RecordDate).Max();
                mostRecentStat = currentStat.SingleOrDefault(c => c.RecordDate == mostRecent);
            }
            else
            {
                mostRecentStat = new Statistic();
            }

            return mostRecentStat;
        }

        private Statistic GetOwnedStartDateStats()
        {
            var ownedStartDateStats = from statistic in Player.Stats
                                      where statistic.RecordDate.Subtract(OwnedStartDate).TotalSeconds <= 0
                                      select statistic;

            // initialize empty stats
            Statistic firstOwnedStat = new Statistic();

            if (ownedStartDateStats.Count() != 0)
            {
                // smallest difference will be the best stat to use
                DateTime firstOwnedStatDate = ownedStartDateStats.Select(s => s.RecordDate).Max();

                // get the stat where the player is owned
                firstOwnedStat = Player.Stats.SingleOrDefault(s => s.RecordDate == firstOwnedStatDate);
            }

            return firstOwnedStat;
        }
    }
}
