﻿using Chappyware.Data.DataObjects;
using Chappyware.Data.DataSources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Chappyware.Data.Storage
{
    public class GameStatStore
    {

        public Dictionary<string, GameStat> HistoricalGames { get; set; }

        public void Load()
        {
            if (HistoricalGames == null)
            {
                string historicalGameStats = File.ReadAllText(DataFileUtilities.GetGameStatFileName());
                HistoricalGames = JsonConvert.DeserializeObject<Dictionary<string, GameStat>>(historicalGameStats);
            }

        }

        public void Save()
        {
            string historicalGameStats = JsonConvert.SerializeObject(HistoricalGames);
            File.WriteAllText(DataFileUtilities.GetGameStatFileName(), historicalGameStats);
        }
    }
}
