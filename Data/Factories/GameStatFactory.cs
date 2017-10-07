using Chappyware.Data.DataObjects;
using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class GameStatFactory
    {

        private static GameStatFactory _Instance;
        private GameStatCollection _AllGameStats;


        public static GameStatFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameStatFactory();
                }
                return _Instance;
            }
        }   

        private GameStatFactory()
        {
            Dictionary<string, GameStat> playerGameStats = StorageFactory.Instance.LoadPersistedGameStats();
            GameStatCollection allGames = new GameStatCollection(playerGameStats.Values.ToList());
            
            _AllGameStats = allGames;
        }
        
        public PlayerGameStatCollection GetPlayerStatCollection(string playerName, string teamCode)
        {
            return _AllGameStats.GetPlayerGameCollection(playerName, teamCode);

        }

    }
}
