using Chappyware.Data.DataObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using Chappyware.Logging;

namespace Chappyware.Data.Storage
{
    public class JsonStorage : IStatStorage
    {
        private List<Player> _PlayerStats;
        private Dictionary<string, GameStat> _GameStats;
        private List<FantasyLeague> _Leagues;

        public List<FantasyLeague> LoadFantasyLeagues()
        {
            if (_Leagues == null)
            {
                string seralizedleagues = File.ReadAllText(DataFileUtilities.GetLeagueFileName());
                _Leagues = JsonConvert.DeserializeObject<List<FantasyLeague>>(seralizedleagues);
            }
            return _Leagues;
        }

        public List<Player> LoadPlayers()
        {
            if (_PlayerStats == null)
            {
                string seralizedPlayerStats = File.ReadAllText(DataFileUtilities.GetStatFileName());
                _PlayerStats = JsonConvert.DeserializeObject<List<Player>>(seralizedPlayerStats);
            }
            return _PlayerStats;
        }

        public void SaveFantasyLeagues(List<FantasyLeague> fantasyLeagues)
        {
            string serializedLeagues = JsonConvert.SerializeObject(fantasyLeagues);
            File.WriteAllText(DataFileUtilities.GetLeagueFileName(), serializedLeagues);
        }
        
        public void SavePlayers(List<Player> players)
        {
            string seralizedPlayers = JsonConvert.SerializeObject(players);
            File.WriteAllText(DataFileUtilities.GetStatFileName(), seralizedPlayers);
        }

        [Obsolete]
        public Dictionary<string, GameStat> LoadGameStats()
        {
            if (_GameStats == null)
            {
                string serializedGameStats = File.ReadAllText(DataFileUtilities.GetGameStatFileName());
                _GameStats = JsonConvert.DeserializeObject<Dictionary<string,GameStat>>(serializedGameStats);
            }
            return _GameStats;
        }

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
                Log.LogStorageEvent($"Game with url '{gameStat.GameUrl}' could not be saved to '{gameJsonFileName}'.  Exception: {e.StackTrace}");
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
                Log.LogStorageEvent($"Game with url '{url}' could not be loaded from '{gameJsonFileName}'.  Exception: {e.StackTrace}");
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
                Log.LogStorageEvent($"Game with url '{gameStat.GameUrl}' could not be updated, reverting '{gameJsonFileName}'.  Exception: {e.StackTrace}");
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
                Log.LogStorageEvent($"Game with url '{gameStat.GameUrl}' could not be deleted: '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }

        public void CreatePlayer(Player newPlayer)
        {

            string playerDatabase = DataFileUtilities.GetPlayerDatabaseFilePath(newPlayer.Id);
            string playerJson = JsonConvert.SerializeObject(newPlayer);

            try
            {
                File.WriteAllText(playerDatabase, playerJson);
            }
            catch (Exception e)
            {
                Log.LogStorageEvent($"Player with name {newPlayer.Name} could not be saved to '{playerDatabase}'.  Exception: {e.StackTrace}");
            }
        }

        public GameStat ReadPlayer(string playerId)
        {
            GameStat readStat = null;
            string playerDatabase = DataFileUtilities.GetPlayerDatabaseFilePath(playerId);
            try
            {
                string serializedGameStats = File.ReadAllText(playerDatabase);
                readStat = JsonConvert.DeserializeObject<GameStat>(serializedGameStats);
            }
            catch (Exception e)
            {
                Log.LogStorageEvent($"Player with ID '{playerId}' could not be loaded from '{playerDatabase}'.  Exception: {e.StackTrace}");
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
                Log.LogStorageEvent($"Game with url '{gameStat.GameUrl}' could not be updated, reverting '{gameJsonFileName}'.  Exception: {e.StackTrace}");
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
                Log.LogStorageEvent($"Game with url '{gameStat.GameUrl}' could not be deleted: '{gameJsonFileName}'.  Exception: {e.StackTrace}");
            }
        }
    }
}
