using Chappyware.Business;
using Chappyware.Data;
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


        }
    }
}
