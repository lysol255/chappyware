using Chappyware.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Data
{
    public class PlayerRecord
    {
        public string Name { get; set; }
        public string CurrentTeam { get; set; }
        public string Id { get; set; }
        public int Age { get; set; }
        
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
