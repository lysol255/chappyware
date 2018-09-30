using System.Collections.Generic;

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
