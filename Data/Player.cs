using Chappyware.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        
        public List<Statistic> Stats { get; set; }
        public PlayerGameStatCollection GameStats { get; set; }

        public int Goals {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.Goals);
            }
        }
        public int Assists {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.Assists);
            }
        }
        public string TOI {
            get
            {
                // need to fix
                return GameStats.PlayerStats.Last().TOI;
            }
        }
        public int PlusMinus {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.PlusMinus);
            }
        }
        public int PenaltyMin {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.PenaltyMin);
            }
        }
        public int Shifts {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.Shifts);
            }
        }
        public int ShortHandedAssists {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.ShortHandedAssists);
            }
        }
        public int PowerPlayAssists {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.PowerPlayAssists);
            }
        }
        public int Shots
        {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.Shots);
            }
        }
        public int EventAssists
        {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.EventAssists);
            }
        }
        public int GameWinningGoals {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.GameWinningGoals);
            }
        }
        public int ShortHandedGoals
        {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.ShortHandedGoals);
            }
        }
        public int PowerPlayGoals
        {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.PowerPlayGoals);
            }
        }
        public int EvenGoals
        {
            get
            {
                return GameStats.PlayerStats.Sum(g => g.EvenGoals);
            }
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



    }
}
