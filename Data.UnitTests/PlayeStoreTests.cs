using Chappyware.Data.Storage;
using Core.Data.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Data.UnitTests
{
    [TestClass]
    public class PlayerStoreTests
    {
        [TestInitialize]
        public void CreateExampleGameStat()
        {
            Shared.CreatePlayerStat();
        }

        [TestMethod]
        public void TestReadPlayer()
        {
            PlayerStore store = new PlayerStore();

            Player p = new Player("David Chapman", "VIC");

            Player readPlayer = store.ReadPlayerRecord(p.Id);
            Assert.IsNotNull(readPlayer);
        }

        [TestMethod]
        public void UpdatePlayer()
        {
            PlayerStore store = new PlayerStore();

            Player p = new Player("David Chapman", "VIC");

            Player backupPlayerState = store.ReadPlayerRecord(p.Id);
            
            PlayerGameStat playerGameStat = new PlayerGameStat();
            playerGameStat.Goals = 1;

            p.AddPlayerGameStat(playerGameStat);

            store.UpdatePlayerRecord(p);

            p = store.ReadPlayerRecord(p.Id);

            Assert.IsTrue(p.GetGoals(DateTime.MinValue,DateTime.MaxValue) == 1);

            // revert to original player stat
            store.UpdatePlayerRecord(backupPlayerState);

        }

    }
}
