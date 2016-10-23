using Chappyware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class FantasyPlayer
    {
        public Player Player { get; set; }

        public DateTime OwnedStartDate { get; set; }

        public DateTime OwnedEndDate { get; set; }
        
    }
}
