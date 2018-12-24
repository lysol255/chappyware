using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Data.DataObjects
{
    public class Player
    {
        public string Name { get; set; }
        public string CurrentTeam { get; set; }
        
        public int Age { get; set; }

        public string Id
        {
            get
            {
                string hash = $"{Regex.Replace(Name, @"\p{Z}", "")}_{CurrentTeam}";
                return hash;
            }
        }

        public PlayerGameStatCollection PlayerGameStats { get; set; }

        #region Constructors

        public Player(string name, string currentTeam)
        {
            PlayerGameStats = new PlayerGameStatCollection();
            Name = name;
            CurrentTeam = currentTeam;
        }

        public Player()
        {

        }

        public Player(string playerName, List<PlayerGameStat> playerGameStats)
        {
            PlayerGameStats = new PlayerGameStatCollection();
            PlayerGameStats.PlayerName = Name;
            PlayerGameStats.PlayerStats = playerGameStats;
        }

        #endregion

        #region Public Methods

        public void AddPlayerGameStat(PlayerGameStat playerGameStat)
        {
            PlayerGameStats.PlayerStats.Add(playerGameStat);
        }
        
        public int GetGamesPlayed(DateTime startDate, DateTime endTime)
        {
            var games = from gameStat in PlayerGameStats.PlayerStats
                        where gameStat.GameDate >= startDate
                        &&
                        gameStat.GameDate < endTime
                        select gameStat;


            return games.Count();
        }

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
                if (PlayerGameStats.PlayerStats.Count > 0)
                {
                    timeOnIce = PlayerGameStats.PlayerStats.Last().TOI;
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

        #endregion

        private List<PlayerGameStat> GetPlayerStatsInRange(DateTime startDate, DateTime endDate)
        {
            var playerStats = from playerStat in PlayerGameStats.PlayerStats
                              where playerStat.GameDate >= startDate
                                  &&
                                  playerStat.GameDate < endDate
                              select playerStat;
            return playerStats.ToList();
        }

    }
}
