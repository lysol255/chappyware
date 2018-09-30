using Chappyware.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Chappyware.Data.Storage
{
    public class PlayerRecordStore
    {

        public void CreatePlayerRecord(PlayerRecord gameStat)
        {

            string playerJsonFileName = DataFileUtilities.GetPlayerDatabaseFilePath(gameStat.Id);
            string playerJson = JsonConvert.SerializeObject(gameStat);

            try
            {
                File.WriteAllText(playerJsonFileName, playerJson);
            }
            catch (Exception e)
            {
                Log.LogStorageEvent($"Game with url '{gameStat.Name}' could not be saved to '{playerJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }

        public PlayerRecord ReadPlayerRecord(int playerId)
        {
            PlayerRecord readStat = null;
            string playerRecord = DataFileUtilities.GetPlayerDatabaseFilePath(playerId.ToString());
            try
            {
                string serializedGameStats = File.ReadAllText(playerRecord);
                readStat = JsonConvert.DeserializeObject<PlayerRecord>(serializedGameStats);
            }
            catch (Exception e)
            {
                Log.LogStorageEvent($"Game with url '{playerId}' could not be loaded from '{playerRecord}'.  Exception: {e.StackTrace}");
            }

            return readStat;
        }

        public void UpdatePlayerRecord(PlayerRecord playerRecord)
        {
            string gameJsonFileName = DataFileUtilities.GetGameStatFilePath(playerRecord.Id);
            try
            {
                string gameJson = JsonConvert.SerializeObject(playerRecord);
                File.WriteAllText(gameJsonFileName, gameJson);
            }
            catch (Exception e)
            {
                Log.LogStorageEvent($"Game with url '{playerRecord.Id}' could not be updated, reverting '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }

        public void DeletePlayerRecord(PlayerRecord playerRecord)
        {
            string gameJsonFileName = DataFileUtilities.GetGameStatFilePath(playerRecord.Id);
            try
            {
                File.Delete(gameJsonFileName);
            }
            catch (Exception e)
            {
                Log.LogStorageEvent($"Game with url '{playerRecord.Id}' could not be deleted: '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }
    }
}
