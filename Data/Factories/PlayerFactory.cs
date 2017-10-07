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

        public Player GetPlayer(string playerName, string teamCode)
        {
            GameStatFactory manager = GameStatFactory.Instance;

            //TODO handle duplicates, maybe not an issue
            Player foundPlayer = new Player();
            foundPlayer.Name = playerName;
            foundPlayer.Team = teamCode;

            foundPlayer.GameStats = manager.GetPlayerStatCollection(playerName, teamCode); 
            return foundPlayer;
        }

    }
}
