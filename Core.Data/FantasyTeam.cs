using Chappyware.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class FantasyTeam
    {
        public Owner Owner { get; set; }

        public List<FantasyPlayer> OwnedPlayers { get; set; }

        public FantasyTeam()
        {
            OwnedPlayers = new List<FantasyPlayer>();
        }
    
        public int GetTotalPoints()
        {
            int totalPoints = 0;
            
            foreach(FantasyPlayer fantasyPlayer in OwnedPlayers)
            {
                if (fantasyPlayer.Player == null)
                {
                    Log.LogEvent($"Could not find a player for fantasy player {fantasyPlayer.PlayerName}");
                    continue;
                }

                totalPoints += fantasyPlayer.Player.GetAssists(fantasyPlayer.OwnedStartDate, fantasyPlayer.OwnedEndDate);
                totalPoints += fantasyPlayer.Player.GetGoals(fantasyPlayer.OwnedStartDate, fantasyPlayer.OwnedEndDate);
            }

            return totalPoints;
        }
    }
}
