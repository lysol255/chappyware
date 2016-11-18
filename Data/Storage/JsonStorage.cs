using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.Storage
{
    public class JsonStorage : IStatStorage
    {


        public List<FantasyLeague> LoadFantasyLeagues()
        {
            string seralizedleagues = File.ReadAllText(DataFileUtilities.GetLeagueFileName());
            List<FantasyLeague> leagues = JsonConvert.DeserializeObject<List<FantasyLeague>>(seralizedleagues);
            return leagues;
        }

        public List<Player> LoadPlayers()
        {
            string seralizedPlayerStats = File.ReadAllText(DataFileUtilities.GetStatFileName());
            List<Player> playerStats = JsonConvert.DeserializeObject< List<Player>>(seralizedPlayerStats);
            return playerStats;
        }

        public void SaveFantasyLeagues(List<FantasyLeague> fantasyLeagues)
        {
            string serializedLeagues = JsonConvert.SerializeObject(fantasyLeagues);
            File.WriteAllText(DataFileUtilities.GetLeagueFileName(), serializedLeagues);
        }

        public void SavePlayers(List<Player> players)
        {
            string seralizedPlayers = JsonConvert.SerializeObject(players);
            File.WriteAllText(DataFileUtilities.GetStatFileName(), seralizedPlayers);
        }
    }
}
