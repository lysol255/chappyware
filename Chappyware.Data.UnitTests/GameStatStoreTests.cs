using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chappyware.Data.Storage;
using System.Collections.Generic;
using Chappyware.Data.DataSources;
using Chappyware.Data.DataObjects;

namespace Chappyware.Data.UnitTests
{
    [TestClass]
    public class GameStatStoreTests
    {
        [TestMethod]
        public void TestUpdatingGameStats()
        {
            GameStatStore store = new GameStatStore();
            store.Load();

            if (store.HistoricalGames == null)
            {
                store.HistoricalGames = new Dictionary<string, GameStat>();
            }

            HockeyReferenceGameStatSource source = new HockeyReferenceGameStatSource(store.HistoricalGames);
            Dictionary<string, GameStat> stats = source.UpdateHistoricalStats();

            store.HistoricalGames = stats;

            store.Save();
        }
    }
}
