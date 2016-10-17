using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.Storage
{
    public class JsonStorage : IStatStorage
    {
        public List<Player> Load()
        {
            string seralizedPlayerStats = File.ReadAllText("Chappystats.txt");
            List<Player> playerStats = JsonConvert.DeserializeObject< List<Player>>(seralizedPlayerStats);
            return playerStats;
        }

        public void Save(List<Player> players)
        {
            string seralizedPlayers = JsonConvert.SerializeObject(players);
            File.WriteAllText("Chappystats.txt", seralizedPlayers);
        }
    }
}
