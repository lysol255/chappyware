using Chappyware.Data.Storage;
using System;

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

        public Player GetPlayer(string playerName, string teamCode)
        {
            JsonStorage store = new JsonStorage();
            Player returnedPlayer = store.Get
        }

        public void CreatePlayer(string playerName, string teamCode, int age)
        {
            Player newPlayer = new Player(playerName, teamCode, age);

            if (!Exists(playerName, teamCode, age))
            {
                newPlayer.Id = Guid.NewGuid().ToString();
            }

            JsonStorage store = new JsonStorage();
            store.CreatePlayer(newPlayer);
            
        }

        private bool Exists(string playerName, string teamCode, int age)
        {
            throw new NotImplementedException();
        }
    }
}
