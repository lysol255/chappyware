using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Chappyware.Data.Storage
{
    public class DataFileUtilities
    {

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
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", GetFileName(gameUrl));

            return currentDirectory;
        }

        public static string GetPlayerDatabaseFilePath(string playerId)
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", $"PlayerDatabase.{player.Id}.json");

            return currentDirectory;
        }

        private static string GetFileName(string gameUrl)
        {
            string[] urlParts = gameUrl.Split('/');

            // something like 201710040EDM.html
            string lastPartOfTheUrl = urlParts.ToList().Last();

            lastPartOfTheUrl = lastPartOfTheUrl.Replace("html", "json");

            return lastPartOfTheUrl;
        }

        public static string GetLeagueFileName()
        {
            string currentDirectory = GetCurrentDirectory();

            return Path.Combine(currentDirectory, "..\\App_Data", _DefaultLeagueFileName);

        }

        private static string GetCurrentDirectory()
        {
            string currentDir = Path.GetDirectoryName(Assembly.GetCallingAssembly().EscapedCodeBase);
            currentDir = currentDir.Replace("file:\\", "");
            return currentDir;
        }

    }
}
