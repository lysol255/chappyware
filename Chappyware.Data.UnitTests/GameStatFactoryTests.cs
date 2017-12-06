using Chappyware.Data;
using System.Linq;
using Chappyware.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Chappyware.Data.UnitTests
{
    [TestClass]
    public class GameStatFactoryTests
    {
        [TestMethod]
        public void GetAllGameTest()
        {

            GameStatFactory gameFactory = GameStatFactory.Instance;

            GameStatCollection games = gameFactory.GetGames();


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

            GameStatCollection games = gameFactory.GetGames();

            GameStat stat = games.GameStats.First();

            int homeGoals = stat.HomeTeamPlayerStats.Values.SingleOrDefault(p => p.Name == "TOTAL").Goals;

            Assert.IsNotNull(homeGoals);
        }
    }
}
