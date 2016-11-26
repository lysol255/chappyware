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

        public List<Statistic> Stats { get; set;}

        public Player()
        {
            Stats = new List<Statistic>();
        }
    }
}
