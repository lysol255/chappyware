using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.DataObjects
{
    public class PlayerGameStatCollection
    {

        public string PlayerName { get; set; }

        public List<PlayerGameStat> PlayerStats { get; set;}

        public PlayerGameStatCollection()
        {
            PlayerStats = new List<PlayerGameStat>();
        }

    }
}
