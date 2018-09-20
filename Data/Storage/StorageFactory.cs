using Chappyware.Data.DataObjects;
using System.Collections.Generic;

namespace Chappyware.Data.Storage
{
    public class StorageFactory
    {

        private IStatStorage _Storage;

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

        private StorageFactory()
        {
            _Storage = new JsonStorage();

        }

        public void UpdatedPersistedStatSource(List<Player> players)
        {
            _Storage.SavePlayers(players);
        }

        public List<Player> LoadPersistedStatSource()
        {
            return _Storage.LoadPlayers();
        }

        public Dictionary<string, GameStat> LoadPersistedGameStats()
        {
            return _Storage.LoadGameStats();
        }

        public void UpdateFantasyTeams(List<FantasyLeague> leagues)
        {
            _Storage.SaveFantasyLeagues(leagues);
        }

        public List<FantasyLeague> LoadPersistedFantasyLeagues()
        {
            return _Storage.LoadFantasyLeagues();
        }

    }
}
