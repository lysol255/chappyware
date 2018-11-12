using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Core.Data.Storage
{
    public class DataFileUtilities
    {
        private const string GameFileSuffix = "game.json";
        private const string PlayerFileSuffix = ".player.json";
        private static string _DefaultStatFileName = "Chappystats.txt";
        private static string _DefaultLeagueFileName = "TeamImport.txt";
        private static string _DefaultGameStats = "GameStats.txt";
        private static string _DefaultLog = "Log.txt";


        private string _StatFileName = string.Empty;
        private string _LeagueFileName = string.Empty;

        public static string GetLeagueStatFileName()
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", _DefaultStatFileName);

            if (!File.Exists(currentDirectory))
            {
                File.CreateText(currentDirectory);
            }

            return currentDirectory;

        }

        public static string GetLogFileName()
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", _DefaultLog);

            return currentDirectory;

        }

        public static string GetGameStatFileName()
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", _DefaultGameStats);

            return currentDirectory;

        }

        public static string GetGameStatFilePath(string gameUrl)
        {
            string currentDirectory = Path.Combine(GetDataFileDirectory(), GetFileName(gameUrl));

            return currentDirectory;
        }

        private static string GetDataFileDirectory()
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..");

            return currentDirectory;
        }

        public static List<string> GetGameStatFiles()
        {
            string currentDirectory = GetDataFileDirectory();

            DirectoryInfo currDirectory = new DirectoryInfo(currentDirectory);
            FileInfo[] gameStatFiles = currDirectory.GetFiles("*"+ GameFileSuffix);
            return gameStatFiles.Select(f => f.FullName).ToList();
        }

        public static string GetPlayerDatabaseFilePath(string playerId)
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", $"PlayerDatabase.{playerId}{PlayerFileSuffix}");

            return currentDirectory;
        }

        public static string GetLeagueFilePath(string leagueName)
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", $"League.{leagueName}.json");

            return currentDirectory;
        }

        private static string GetFileName(string gameUrl)
        {
            string[] urlParts = gameUrl.Split('/');

            // something like 201710040EDM.html
            string lastPartOfTheUrl = urlParts.ToList().Last();

            lastPartOfTheUrl = lastPartOfTheUrl.Replace("html", GameFileSuffix);

            return lastPartOfTheUrl;
        }

        private static string GetCurrentDirectory()
        {
            string currentDir = Path.GetDirectoryName(Assembly.GetCallingAssembly().EscapedCodeBase);
            currentDir = currentDir.Replace("file:\\", "");
            return currentDir;
        }

    }
}
