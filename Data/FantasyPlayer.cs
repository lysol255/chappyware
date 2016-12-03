﻿using Chappyware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class FantasyPlayer
    {
        public FantasyPlayer(Player player)
        {
            Player = player;

 


        }

        public Player Player { get; set; }

        public DateTime OwnedStartDate { get; set; }

        public DateTime OwnedEndDate { get; set; }

        public int DraftRound { get; set; }

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
