using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Chappyware.Data;
using Core.Data.DataObjects;
using Core.Data.Storage;

namespace Data.UnitTests
{
    [TestClass]
    public class GameStatFactoryTests
    {

        [TestInitialize]
        public void CreateExampleGameStat()
        {
            Shared.CreateGameStat();
        }

        [TestMethod]
        public void GetAllGameTest()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            List<GameStat> games = gameFactory.GetGames();
            
            Assert.IsNotNull(games);
        }

        [TestMethod]
        public void GetGameById()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            GameStat game = gameFactory.GetGame("http://www.hockey-reference.com/boxscores/201710200ANA.html");


            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GetGamesForTeam()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            List<GameStat> games = gameFactory.FindGamesForTeam("ANA");
            
            Assert.IsNotNull(games);
        }

        [TestMethod]
        public void GetGamesForPlayer()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            List<GameStat> games = gameFactory.FindGamesForPlayer("SidneyCrosby");

            Assert.IsNotNull(games);
        }

        [TestMethod]
        public void GetHomeTeamGoals()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            List<GameStat> games = gameFactory.GetGames();

            GameStat stat = games.First();

            int homeGoals = stat.HomeTeamPlayerStats.Values.SingleOrDefault(p => p.Name == "TOTAL").Goals;

            Assert.IsNotNull(homeGoals);
        }
    }
}
