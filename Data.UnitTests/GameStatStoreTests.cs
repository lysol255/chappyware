using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Core.Data.Storage;
using Core.Data.DataObjects;

namespace Data.UnitTests
{
    [TestClass]
    public class GameStatStoreTests
    {
        
        [TestInitialize]
        public void CreateExampleGameStat()
        {
            Shared.CreateGameStat();
        }

        [TestMethod]
        public void TestReadGameStat()
        {
            GameStatStore store = new GameStatStore();
            GameStat stat = store.ReadGameStat(Shared.GAME_URL);
            Assert.IsNotNull(stat);
        }

        [TestMethod]
        public void TestUpdateStat()
        {
            GameStatStore store = new GameStatStore();
            GameStat stat = store.ReadGameStat(Shared.GAME_URL);

            string backupJson = JsonConvert.SerializeObject(stat);
            GameStat backup = JsonConvert.DeserializeObject<GameStat>(backupJson);

            stat.AwayTeamCode = "TEST";

            store.UpdateGameStat(stat);
            stat = null;
            stat = store.ReadGameStat(Shared.GAME_URL);

            Assert.IsTrue(stat.AwayTeamCode.Equals("TEST"));

            // reset back to original state
            store.UpdateGameStat(backup);
        }


    }
}
