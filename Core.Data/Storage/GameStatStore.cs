using Chappyware.Logging;
using Core.Data.DataObjects;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Core.Data.Storage
{
    public class GameStatStore
    {        
     
        public void CreateGameStat(GameStat gameStat)
        {

            string gameJsonFileName = DataFileUtilities.GetGameStatFilePath(gameStat.GameUrl);
            string gameJson = JsonConvert.SerializeObject(gameStat);

            try
            {
                File.WriteAllText(gameJsonFileName, gameJson);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{gameStat.GameUrl}' could not be saved to '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }

        public GameStat ReadGameStat(string url)
        {
            GameStat readStat = null;
            string gameJsonFileName = DataFileUtilities.GetGameStatFilePath(url);
            try
            {
                string serializedGameStats = File.ReadAllText(gameJsonFileName);
                readStat = JsonConvert.DeserializeObject<GameStat>(serializedGameStats);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{url}' could not be loaded from '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }

            return readStat;
        }

        public void UpdateGameStat(GameStat gameStat)
        {
            string gameJsonFileName = DataFileUtilities.GetGameStatFilePath(gameStat.GameUrl);
            try
            {
                string gameJson = JsonConvert.SerializeObject(gameStat);
                File.WriteAllText(gameJsonFileName, gameJson);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{gameStat.GameUrl}' could not be updated, reverting '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }

        public void DeleteGameStat(GameStat gameStat)
        {
            string gameJsonFileName = DataFileUtilities.GetGameStatFilePath(gameStat.GameUrl);
            try
            {
                File.Delete(gameJsonFileName);
            }
            catch (Exception e)
            {
                Log.LogEvent($"Game with url '{gameStat.GameUrl}' could not be deleted: '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }
    }
}
