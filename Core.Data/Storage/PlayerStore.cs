using Chappyware.Logging;
using Core.Data.DataObjects;
using Core.Data.Storage;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Chappyware.Data.Storage
{
    public class PlayerStore
    {

        public void CreatePlayerRecord(Player gameStat)
        {
            UpdatePlayerRecord(gameStat);
        }

        public Player ReadPlayerRecord(string playerId)
        {
            Player readStat = null;
            string playerRecord = DataFileUtilities.GetPlayerDatabaseFilePath(playerId);
            try
            {
                string serializedGameStats = File.ReadAllText(playerRecord);
                readStat = JsonConvert.DeserializeObject<Player>(serializedGameStats);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{playerId}' could not be loaded from '{playerRecord}'.  Exception: {e.StackTrace}");
            }

            return readStat;
        }

        public void UpdatePlayerRecord(Player playerRecord)
        {
            string playerDatabaseFileName = DataFileUtilities.GetPlayerDatabaseFilePath(playerRecord.Id);
            try
            {
                string playerJson = JsonConvert.SerializeObject(playerRecord);
                File.WriteAllText(playerDatabaseFileName, playerJson);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{playerRecord.Id}' could not be updated, reverting '{playerDatabaseFileName}'.  Exception: {e.StackTrace}");
            }
        }

        public void DeletePlayerRecord(Player playerRecord)
        {
            string gameJsonFileName = DataFileUtilities.GetPlayerDatabaseFilePath(playerRecord.Id);
            try
            {
                File.Delete(gameJsonFileName);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{playerRecord.Id}' could not be deleted: '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }
    }
}
