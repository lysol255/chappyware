using Chappyware.Data.Storage;
using Chappyware.Logging;
using Core.Data.DataObjects;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Core.Data.Storage
{
    public class LeagueStore
    {

        public League ReadLeague(string leagueId)
        {
            League storedLeague = null;

            string storeLeagueFilePath = DataFileUtilities.GetLeagueFilePath(leagueId);
            try
            {
                string serializedLeague = File.ReadAllText(storeLeagueFilePath);
                storedLeague = JsonConvert.DeserializeObject<League>(serializedLeague);
            }
            catch (Exception e)
            {
                Log.LogEvent($"League with name '{leagueId}' could not be loaded from '{storeLeagueFilePath}'.  Exception: {e.StackTrace}");
            }

            return storedLeague;
        }

        public void CreateLeague(League newLeague)
        {
            string storeLeagueFilePath = DataFileUtilities.GetLeagueFilePath(newLeague.Name);
            try
            {
                string leagueJson = JsonConvert.SerializeObject(newLeague);
                File.WriteAllText(storeLeagueFilePath, leagueJson);
            }
            catch (Exception e)
            {
                Log.LogEvent($"League with name '{newLeague.Name}' could not be updated, reverting '{storeLeagueFilePath}'.  Exception: {e.StackTrace}");
            }
        }

    }
}
