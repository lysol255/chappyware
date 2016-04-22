using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class Player
    {
        public int Goals { get; set; }
        public int GamesPlayed { get; set; }
        public int Assists { get; set; }
        public int GoalsFor { get; set; }
        public int PowerPlayPoints { get; set; }
        public int Shots { get; set; }
        public double Fenwick { get; set; }
        public double Corsi { get; set; }
        public string Name { get; internal set; }
        public string Team { get; internal set; }
    }
}
