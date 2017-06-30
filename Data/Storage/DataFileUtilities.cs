using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.Storage
{
    public class DataFileUtilities
    {

        private static string _DefaultStatFileName = "Chappystats.txt";
        private static string _DefaultLeagueFileName = "TeamImport.txt";

        private string _StatFileName = string.Empty;
        private string _LeagueFileName = string.Empty;

        public static string GetStatFileName()
        {
            string currentDirectory = GetCurrentDirectory();

            currentDirectory = Path.Combine(currentDirectory, "..", _DefaultStatFileName);

            return currentDirectory;

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
