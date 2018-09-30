﻿using System.IO;
using Newtonsoft.Json;
using Chappyware.Data.Storage;
using Chappyware.Data.DataObjects;
using Xunit;

namespace Chappyware.Data.UnitTests
{
    [Xunit.]
    public class GameStatStoreTests
    {
        private string gameUrl = "http://www.hockey-reference.com/boxscores/201710050OTT.html";
        
        [TestInitialize]
        public void CreateExampleGameStat()
        {
            HockeyReferenceGameStatSource statSource = new HockeyReferenceGameStatSource();
            
            // ensure that the example stat file exists for the tests
            if (!File.Exists(DataFileUtilities.GetGameStatFilePath(gameUrl)))
            {
                GameStat exampleStat = statSource.ProcessGame(gameUrl);

                string serializedGameStats = JsonConvert.SerializeObject(exampleStat);
                File.WriteAllText(DataFileUtilities.GetGameStatFilePath(gameUrl), serializedGameStats);
            }
            
        }
        public void TestReadGameStat()
        {
            GameStatStore store = new GameStatStore();
            GameStat stat = store.ReadGameStat(gameUrl);
            Assert.IsNotNull(stat);
        }


    }
}
