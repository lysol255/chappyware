using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.Storage
{
    public class StorageFactory
    {

        private static StorageFactory _Instance;

        public static StorageFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new StorageFactory();
                }
                return _Instance;
            }
        }

        public void UpdatedPersistedStatSource(List<Player> players)
        {
            IStatStorage storage = new JsonStorage();
            storage.SavePlayers(players);
        }

        public List<Player> LoadPersistedStatSource()
        {
            IStatStorage storage = new JsonStorage();
            return storage.LoadPlayers();
        }

        public void UpdateFantasyTeams(List<FantasyLeague> leagues)
        {
            IStatStorage storage = new JsonStorage();
            storage.SaveFantasyLeagues(leagues);
        }

        public List<FantasyLeague> LoadPersistedFantasyLeagues()
        {
            IStatStorage storage = new JsonStorage();
            return storage.LoadFantasyLeagues();
        }

    }
}
