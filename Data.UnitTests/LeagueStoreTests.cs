using Core.Data;
using Core.Data.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Data.UnitTests
{
    [TestClass]
    public class LeagueStoreTests
    {

        [TestMethod]
        public void TestCreateLeague()
        {
            League newLeague = new League();
            newLeague.LeagueTeamFile = "TeamImport.txt";
            newLeague.Name = "Robs";

            LeagueStore leagueStore = new LeagueStore();
            leagueStore.CreateLeague(newLeague);

            Assert.IsTrue(File.Exists(DataFileUtilities.GetLeagueFilePath(newLeague.Name)));

        }

        [TestMethod]
        public void TestReadLeague()
        {
            LeagueStore leagueStore = new LeagueStore();
            League storedLeague = leagueStore.ReadLeague("Robs");

            Assert.IsNotNull(storedLeague);
        }

        [TestMethod]
        public void TestGetTeamsFromLeague()
        {
            LeagueStore leagueStore = new LeagueStore();
            League storedLeague = leagueStore.ReadLeague("Robs");

            List<FantasyTeam> teams = storedLeague.GetTeams();
        }

    }
}
