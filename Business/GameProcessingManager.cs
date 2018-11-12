using Chappyware.Data;
using Chappyware.Data.DataSources;
using Chappyware.Data.Factories;
using Core.Data.DataObjects;
using Core.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Business
{
    public class GameProcessingManager
    {
        public void DownloadAndCreateGameFiles()
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

        public void UpdatePlayerStatFiles()
        {
            GameStatFactory gameFactory = GameStatFactory.Instance;

            PlayerFactory playerFactory = PlayerFactory.Instance;
            
            List<GameStat> allStoredGames = gameFactory.GetGames();

            foreach(GameStat game in allStoredGames)
            {
                foreach(string playerName in game.AwayTeamPlayerStats.Keys)
                {
                    // skip the 'total' player game stats
                    if (playerName.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase)) continue;

                    // get the player
                    PlayerGameStat playerGameStat = game.AwayTeamPlayerStats[playerName];
                    Player updatePlayer = playerFactory.GetPlayer(playerName, game.AwayTeamCode);

                    // create the player record if new
                    if(updatePlayer == null)
                    {
                        updatePlayer = playerFactory.CreatePlayer(playerGameStat.Name, playerGameStat.TeamCode);
                    }

                    // add the game stat and store
                    updatePlayer.AddPlayerGameStat(playerGameStat);
                    playerFactory.UpdatePlayer(updatePlayer);
                }

            }
        }

        private List<string> FilterOutAlreadyStoredGames(List<string> allGameUrls)
        {
            GameStatFactory gameStatFactory = GameStatFactory.Instance;
            List<GameStat> allStoredGameStats = gameStatFactory.GetGames();

            var newGameUrls = allGameUrls.Where(url => !allStoredGameStats.Any(exitingUrl => exitingUrl.Equals(url)));
            return newGameUrls.ToList();
        }
    }
}
