using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class Player
    {
        public string Name { get; internal set; }
        public string Team { get; internal set; }

        public List<Statistics> Stats { get; set;}

        public Player()
        {
            Stats = new List<Statistics>();
        }
    }
}
