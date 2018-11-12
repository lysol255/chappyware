using Chappyware.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class GameProcessingTests
    {
        [TestMethod]
        public void TestDownloadAndProcessNewGames()
        {

            GameProcessingManager manager = new GameProcessingManager();

            manager.DownloadAndCreateGameFiles();


        }

        [TestMethod]
        public void TestUpdatePlayerStatFiles()
        {
            GameProcessingManager manager = new GameProcessingManager();
            manager.UpdatePlayerStatFiles();            
        }
    }
}
