using Chappyware.Data;
using Core.Data.DataObjects;
using Core.Data.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Chappyware.Business;

namespace Business.Tests
{
    [TestClass]
    public class GameProcessingTests
    {
        [TestMethod]
        public void TestDownloadAndProcessNewGames()
        {

            GameProcessingManager manager = new GameProcessingManager();

            manager.DownloadAndCreateGameFiles();


        }

        [TestMethod]
        public void TestUpdatePlayerStatFiles()
        {
            GameProcessingManager manager = new GameProcessingManager();
            manager.UpdatePlayerStatFiles();            
        }

        [TestMethod]
        public void FixGameDayInStoredGames()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            GameStatStore store = new GameStatStore();

            List<GameStat> games = gameFactory.GetGames();

            foreach (GameStat game in games)
            {
                foreach (PlayerGameStat stat in game.AwayTeamPlayerStats.Values)
                {
                    stat.GameDate = game.GetGameDate();
                }

                foreach (PlayerGameStat stat in game.HomeTeamPlayerStats.Values)
                {
                    stat.GameDate = game.GetGameDate();
                }

                store.UpdateGameStat(game);
            }

            Assert.IsNotNull(games);
        }
    }
}
