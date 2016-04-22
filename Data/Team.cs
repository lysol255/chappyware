using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class Team
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public int OvertimeLosses { get; set; }
        public int Losses { get; set; }
        public string Conference { get; set; }
    }
}
