using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.Factories
{
    public class PlayerFactory
    {
        private static PlayerFactory _Instance;

        public static PlayerFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PlayerFactory();
                }
                return _Instance;
            }
        }

        private PlayerFactory()
        {

        }

        public Player GetPlayerByName(string playerName)
        {
            List<Player> players = StorageFactory.Instance.LoadPersistedStatSource();
            //TODO handle duplicates
            Player foundPlayer = players.SingleOrDefault(p => p.Name.Equals(playerName));
            return foundPlayer;
        }

    }
}
