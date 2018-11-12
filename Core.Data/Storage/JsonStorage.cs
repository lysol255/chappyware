﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using Core.Data.DataObjects;
using Chappyware.Data;

namespace Core.Data.Storage
{
    public class JsonStorage
    {
        private List<Player> _PlayerStats;
        private Dictionary<string, GameStat> _GameStats;
        private List<FantasyLeague> _Leagues;

        public List<Player> LoadPlayers()
        {
            if (_PlayerStats == null)
            {
                string seralizedPlayerStats = File.ReadAllText(DataFileUtilities.GetLeagueStatFileName());
                _PlayerStats = JsonConvert.DeserializeObject<List<Player>>(seralizedPlayerStats);
            }
            return _PlayerStats;
        }
       
        public void SavePlayers(List<Player> players)
        {
            string seralizedPlayers = JsonConvert.SerializeObject(players);
            File.WriteAllText(DataFileUtilities.GetLeagueStatFileName(), seralizedPlayers);
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

        
    }
}
