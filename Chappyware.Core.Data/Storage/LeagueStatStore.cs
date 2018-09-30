using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Chappyware.Data.Storage
{
    public class LeagueStatStore
    {
        /// <summary>
        /// the Key is the player name+TeamName
        /// </summary>
        public Dictionary<string, Statistic> LeagueStats { get; set; }

        public void Load()
        {
            if (LeagueStats == null)
            {
                string leagueStats = File.ReadAllText(DataFileUtilities.GetLeagueStatFileName());
                LeagueStats = JsonConvert.DeserializeObject<Dictionary<string, Statistic>>(leagueStats);
            }

        }

        public void Save()
        {
            string leagueStats = JsonConvert.SerializeObject(LeagueStats);
            File.WriteAllText(DataFileUtilities.GetLeagueStatFileName(), leagueStats);
        }
    }
}
