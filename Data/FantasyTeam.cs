using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class FantasyTeam
    {
        public Owner Owner { get; set; }

        public List<FantasyPlayer> OwnedPlayers { get; set; }

        public FantasyTeam()
        {
            OwnedPlayers = new List<FantasyPlayer>();
        }
    
    }
}
