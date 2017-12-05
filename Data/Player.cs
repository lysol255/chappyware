using Chappyware.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Data
{
    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        
        public List<Statistic> Stats { get; set; }
        public PlayerGameStatCollection GameStats { get; set; }

        public int GetGoals (DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s=>s.Goals);
        }

        public int GetAssists(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.Assists);
        }

        public string TOI {
            get
            {
                // need to fix
                string timeOnIce = "0:00";
                if (GameStats.PlayerStats.Count > 0)
                {
                    timeOnIce = GameStats.PlayerStats.Last().TOI;
                }
                return timeOnIce;
            }
        }

        public int GetPlusMinus(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.PlusMinus);
        }

        public int GetPenaltyMin(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.PenaltyMin);
        }

        public int GetShifts(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.Shifts);
        }

        public int GetShortHandedAssists(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.ShortHandedAssists);
        }

        public int GetPowerPlayAssists(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.PowerPlayAssists);
        }

        public int GetShots(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.Shots);
        }

        public int GetEventAssists(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.EventAssists);
        }

        public int GetGameWinningGoals(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.GameWinningGoals);
        }

        public int GetShortHandedGoals(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.ShortHandedGoals);
        }

        public int GetPowerPlayGoals(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.PowerPlayGoals);
        }

        public int GetEvenGoals(DateTime startDate, DateTime endDate)
        {
            var playerStats = GetPlayerStatsInRange(startDate, endDate);

            return playerStats.Sum(s => s.EvenGoals);
        }

        public Player()
        {
            Stats = new List<Statistic>();
            GameStats = new PlayerGameStatCollection();
        }

        public Player(string playerName, List<PlayerGameStat> playerGameStats)
        {
            Stats = new List<Statistic>();
            GameStats = new PlayerGameStatCollection();
            GameStats.PlayerName = Name;
            GameStats.PlayerStats = playerGameStats;
        }

        private List<PlayerGameStat> GetPlayerStatsInRange(DateTime startDate, DateTime endDate)
        {
            var playerStats = from playerStat in GameStats.PlayerStats
                              where playerStat.GameDate >= startDate
                                  &&
                                  playerStat.GameDate < endDate
                              select playerStat;
            return playerStats.ToList();
        }

    }
}
