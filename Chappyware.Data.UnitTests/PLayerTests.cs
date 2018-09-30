using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chappyware.Data.Factories;
using System.Linq;

namespace Chappyware.Data.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void GetGoals()
        {

            PlayerFactory factory = PlayerFactory.Instance;

            Player p = factory.GetPlayer("Duncan Keith", "CHI");

            int goals = p.GetGoals(Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR), DateTime.Today);
            int assists = p.GetAssists(Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR), DateTime.Today);


        }

        [TestMethod]
        public void TestTrade()
        {

            PlayerFactory factory = PlayerFactory.Instance;

            Player p = factory.GetPlayer("Mathew Barzal", "NYI");

            int goals = p.GetGoals(Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR), DateTime.Today);
            int assists = p.GetAssists(Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR), DateTime.Today);

            int goalsFromTrade = p.GetGoals(new DateTime(2017,11,30), DateTime.Today);
            int assistsFromTrade = p.GetAssists(new DateTime(2017, 11, 30), DateTime.Today);


        }


        [TestMethod]
        public void CompareLeagueStatWithGameStatTotal()
        {

            PlayerFactory factory = PlayerFactory.Instance;

            Player p = factory.GetPlayer("Duncan Keith", "CHI");

            DateTime startOfSeason = new DateTime(2017, 10, 4);

            int goals = p.GetGoals(startOfSeason, DateTime.Today);
            int assists = p.GetAssists(startOfSeason, DateTime.Today);

            //Assert.AreEqual(goals, p.Stats.Last().Goals);
            //Assert.AreEqual(assists, p.Stats.Last().Assists);

        }


        [TestMethod]
        public void ReprocessPlayer()
        {

            PlayerFactory factory = PlayerFactory.Instance;

            Player p = factory.GetPlayer("Duncan Keith", "CHI");

            GameStatFactory gameStatFactory = GameStatFactory.Instance;
            gameStatFactory.ReprocessGamesForPlayer(p.Name, p.CurrentTeam);

        }
    }
}
