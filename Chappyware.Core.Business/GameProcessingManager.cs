using Chappyware.Data;
using Chappyware.Data.DataObjects;
using Chappyware.Data.DataSources;
using Chappyware.Data.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Business
{
    class GameProcessingManager
    {
        public void ProcessAllNewGames()
        {
            HockeyReferenceGameStatSource statSource = new HockeyReferenceGameStatSource();
            GameStatStore gameStore = new GameStatStore();


            // get game urls
            List<string> allGameUrls = statSource.GetGameUrls();

            // only process new game Urls
            List<string> newGameUrls = FilterOutAlreadyStoredGames(allGameUrls);

            foreach (string gameUrl in newGameUrls)
            {
                GameStat gameStat = null;

                // has this game already been recorded
                if (GameStatFactory.Instance.GetGame(gameUrl) != null)
                {
                    continue;
                }

                // create game stats
                gameStat = statSource.ProcessGame(gameUrl);

                // add the game if it processed correctly
                if (gameStat != null)
                {
                    gameStore.CreateGameStat(gameStat);
                }
            }
        }

        private List<string> FilterOutAlreadyStoredGames(List<string> allGameUrls)
        {
            GameStatFactory gameStatFactory = GameStatFactory.Instance;
            GameStatCollection allStoredGameStats = gameStatFactory.GetGames();

            var newGameUrls = allGameUrls.Where(url => !allStoredGameStats.GameStats.Any(exitingUrl => exitingUrl.Equals(url)));
            return newGameUrls.ToList();
        }
    }
}
