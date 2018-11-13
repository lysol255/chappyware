using Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Business.Tests
{
    [TestClass]
    public class FantasyTeamManagerTests
    {
        [TestMethod]
        public void TestGetFantasyTeams()
        {

            FantasyTeamManager manager = FantasyTeamManager.Insance;
            string leagueName = "Robs";

            List<FantasyTeam> teams = manager.GetTeamsForLeague(leagueName);

            Assert.IsTrue(teams.Count == 9);

        }

        [TestMethod]
        public void TestGetTotalPoints()
        {

            FantasyTeamManager manager = FantasyTeamManager.Insance;
            string leagueName = "Robs";

            List<FantasyTeam> teams = manager.GetTeamsForLeague(leagueName);

            int totalPoints = teams[7].GetTotalPoints();


        }
    }
}
