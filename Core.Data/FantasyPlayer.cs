using Chappyware.Data.Factories;
using Core.Data.DataObjects;
using System;

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
                return 0;
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
            throw new NotImplementedException();

        }

        private Statistic GetOwnedStartDateStats()
        {
            throw new NotImplementedException();
        }
    }
}
