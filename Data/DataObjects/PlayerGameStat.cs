using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class PlayerGameStat
    {

        public string Name { get; set; }

        public string TeamCode { get; set; }

        public int Goals { get; set; }
        public int Assists { get; set; }
        public string TOI { get; set; }
        public int PlusMinus { get; internal set; }
        public int PenaltyMin { get; internal set; }
        public int Shifts { get; internal set; }
        public int ShortHandedAssists { get; internal set; }
        public int PowerPlayAssists { get; internal set; }
        public int Shots { get; internal set; }
        public int EventAssists { get; internal set; }
        public int GameWinningGoals { get; internal set; }
        public int ShortHandedGoals { get; internal set; }
        public int PowerPlayGoals { get; internal set; }
        public int EvenGoals { get; internal set; }
    }
}
