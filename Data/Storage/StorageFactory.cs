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
            storage.Save(players);
        }

        public List<Player> LoadPersistedStatSource()
        {
            IStatStorage storage = new JsonStorage();
            return storage.Load();
        }

    }
}
