using Chappyware.Data.DataSources;
using Core.Data.DataObjects;
using Core.Data.Storage;
using Newtonsoft.Json;
using System.IO;

namespace Data.UnitTests
{
    class Shared
    {
        public const string GAME_URL = "https://www.hockey-reference.com/boxscores/201810030SJS.html";


        public static void CreateGameStat()
        {
            HockeyReferenceGameStatSource statSource = new HockeyReferenceGameStatSource();

            string gameFileName = DataFileUtilities.GetGameStatFilePath(GAME_URL);

            // ensure that the example stat file exists for the tests
            if (!File.Exists(gameFileName))
            {
                GameStat exampleStat = statSource.ProcessGame(GAME_URL);

                string serializedGameStats = JsonConvert.SerializeObject(exampleStat);
                File.WriteAllText(gameFileName, serializedGameStats);
            }
        }

        public static void CreatePlayerStat()
        {
            Player p = new Player("David Chapman", "VIC");

            string playerFileName = DataFileUtilities.GetPlayerDatabaseFilePath(p.Id);

            // ensure that the example stat file exists for the tests
            if (!File.Exists(playerFileName))
            {
                string serializedGameStats = JsonConvert.SerializeObject(p);
                File.WriteAllText(playerFileName, serializedGameStats);
            }
        }
    }
}
