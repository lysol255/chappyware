using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class PlayerGameStats
    {

        public string Name { get; set; }

        public string TeamCode { get; set; }

        public int Goals { get; set; }
        public int Assists { get; set; }
        public string TOI { get; set; }
        public int PlusMinus { get; internal set; }
        public int PenaltyMin { get; internal set; }
    }
}
