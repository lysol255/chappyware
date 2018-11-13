using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.DataObjects
{
    public class PlayerGameStatCollection
    {

        public string PlayerName { get; set; }

        public List<PlayerGameStat> PlayerStats { get; set;}

        public PlayerGameStatCollection()
        {
            PlayerStats = new List<PlayerGameStat>();
        }

        public void SetGameStat(PlayerGameStat playerGameStat)
        {
            // find by day
            PlayerGameStat foundExistingPlayerGameStat = PlayerStats.SingleOrDefault(pg => pg.GameDate.CompareTo(playerGameStat.GameDate) == 0);
            if (foundExistingPlayerGameStat != null)
            {
                // replace
                foundExistingPlayerGameStat = playerGameStat;
            }
            else
            {
                PlayerStats.Add(playerGameStat);
            }
        }
    }
}
