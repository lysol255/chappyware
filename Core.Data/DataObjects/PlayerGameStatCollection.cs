using System.Collections.Generic;

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

    }
}
