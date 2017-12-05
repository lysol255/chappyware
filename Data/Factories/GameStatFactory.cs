using Chappyware.Data.DataObjects;
using Chappyware.Data.Storage;
using System.Collections.Generic;
using System.Linq;

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

        public GameStatCollection GetGames()
        {
            return _AllGameStats;
        }

        public GameStat GetGame(string gameId)
        {
            GameStat gameStats = _AllGameStats.GameStats.SingleOrDefault(g => g.GameUrl == gameId);
            return gameStats;
        }

        public List<GameStat> FindGamesForTeam(string teamCode)
        {
            var games = _AllGameStats.GameStats.Where(g => g.AwayTeamCode == teamCode
                    ||
                    g.HomeTeamCode == teamCode);
            return games.ToList();
        }

        public List<GameStat> FindGamesForPlayer(string playerName)
        {
            List<GameStat> playerGames = new List<GameStat>();
            string normalizedSearchPlayerName = playerName.ToLowerInvariant();

            foreach (GameStat gameStat in _AllGameStats.GameStats)
            {

                if (gameStat.AwayTeamPlayerStats.Where(g=> NormalizePlayerName(g.Key) == normalizedSearchPlayerName).Count() > 0)
                {
                    playerGames.Add(gameStat);
                }

                if (gameStat.HomeTeamPlayerStats.Where(g => NormalizePlayerName(g.Key) == normalizedSearchPlayerName).Count() > 0)
                {
                    playerGames.Add(gameStat);
                }
            }

            return playerGames;
        }

        /// <summary>
        /// Takes a name with spaces and makes it into a single Key
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string NormalizePlayerName(string name)
        {
            string normalizedName = name.Replace(" ", "");
            normalizedName = normalizedName.Trim();
            normalizedName = normalizedName.ToLowerInvariant();
            return normalizedName;
        }
    }
}
