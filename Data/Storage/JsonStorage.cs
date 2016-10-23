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
        public List<FantasyLeague> LoadFantasyLeagues()
        {
            string seralizedleagues = File.ReadAllText("Chappyleagues.txt");
            List<FantasyLeague> leagues = JsonConvert.DeserializeObject<List<FantasyLeague>>(seralizedleagues);
            return leagues;
        }

        public List<Player> LoadPlayers()
        {
            string seralizedPlayerStats = File.ReadAllText("Chappystats.txt");
            List<Player> playerStats = JsonConvert.DeserializeObject< List<Player>>(seralizedPlayerStats);
            return playerStats;
        }

        public void SaveFantasyLeagues(List<FantasyLeague> fantasyLeagues)
        {
            string serializedLeagues = JsonConvert.SerializeObject(fantasyLeagues);
            File.WriteAllText("Chappyleagues.txt", serializedLeagues);
        }

        public void SavePlayers(List<Player> players)
        {
            string seralizedPlayers = JsonConvert.SerializeObject(players);
            File.WriteAllText("Chappystats.txt", seralizedPlayers);
        }
    }
}
