using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Core.Data.DataObjects;
using Core.Data.Storage;

namespace Chappyware.Data
{
    public class GameStatFactory
    {

        private static GameStatFactory _Instance;
        private List<GameStat> _GameStats;

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
            _GameStats = new List<GameStat>();
            List<string> storedGameFiles = DataFileUtilities.GetGameStatFiles();
            foreach(string fileName in storedGameFiles)
            {
                // read all saved gamestats and make gamestat objects
                string serializedGameStats = File.ReadAllText(fileName);
                GameStat storedGameStat = JsonConvert.DeserializeObject<GameStat>(serializedGameStats);
                _GameStats.Add(storedGameStat);
            }

        }
        
        public List<GameStat> GetGames()
        {
            return _GameStats;
        }

        public GameStat GetGame(string gameUrl)
        {
            GameStat gameStats = _GameStats.SingleOrDefault(g => g.GameUrl == gameUrl);
            return gameStats;
        }

        public List<GameStat> FindGamesForTeam(string teamCode)
        {
            var games = _GameStats.Where(g => g.AwayTeamCode == teamCode
                    ||
                    g.HomeTeamCode == teamCode);
            return games.ToList();
        }

        //public void ReprocessGamesForPlayer(string playerName, string teamCode)
        //{
        //    List<GameStat> playerGames = FindGamesForPlayer(NormalizePlayerName(playerName));

        //    HockeyReferenceGameStatSource statSource = new HockeyReferenceGameStatSource();

        //    Dictionary<string, GameStat> playerGameStats = StorageFactory.Instance.LoadPersistedGameStats();

        //    GameStat correctedGameStat = null;

        //    // reprocess the games
        //    foreach (GameStat game in playerGames)
        //    {
        //        correctedGameStat = statSource.ProcessGame(game.GameUrl);

        //        if (playerGameStats.ContainsKey(game.GameUrl))
        //        {
        //            playerGameStats[game.GameUrl] = correctedGameStat;
        //        }

        //        //stop reprocessing if the player stats are now correct
        //        if(ArePlayerStatsCorrect(playerName, teamCode))
        //        {
        //            break;
        //        }
        //    }

        //    // save back the corrections

        //    StorageFactory.Instance.SavePersistedGameStats(playerGameStats);

        //}

        private bool ArePlayerStatsCorrect(string playerName, string teamCode)
        {
            bool statsAreValid = false;

            //PlayerFactory factory = PlayerFactory.Instance;
            //Player thePlayer = factory.GetPlayer(playerName, teamCode);

            //int goals = thePlayer.GetGoals(Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR), DateTime.Today);
            //int assists = thePlayer.GetAssists(Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR), DateTime.Today);

            //if (goals == thePlayer.Stats.Last().Goals
            //    &&
            //    assists == thePlayer.Stats.Last().Assists)
            //{
            //    statsAreValid = true;    
            //}

            return statsAreValid;
        }

        public List<GameStat> FindGamesForPlayer(string playerName)
        {
            List<GameStat> playerGames = new List<GameStat>();
            string normalizedSearchPlayerName = playerName.ToLowerInvariant();

            foreach (GameStat gameStat in _GameStats)
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
