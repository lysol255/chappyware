﻿using Chappyware.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.Storage
{
    public class StorageFactory
    {

        private IStatStorage _Storage;

        private static StorageFactory _Instance;

        public static StorageFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new StorageFactory();
                }
                return _Instance;
            }
        }

        private StorageFactory()
        {
            _Storage = new JsonStorage();

        }

        public void UpdatedPersistedStatSource(List<Player> players)
        {
            _Storage.SavePlayers(players);
        }

        public List<Player> LoadPersistedStatSource()
        {
            return _Storage.LoadPlayers();
        }

        public Dictionary<string, GameStat> LoadPersistedGameStats()
        {
            return _Storage.LoadGameStats();
        }

        public void SavePersistedGameStats(Dictionary<string, GameStat> gameStats)
        {
            GameStatStore gameStatStore = new GameStatStore();
            gameStatStore.HistoricalGames = gameStats;
            gameStatStore.Save();

        }

        public void UpdateFantasyTeams(List<FantasyLeague> leagues)
        {
            _Storage.SaveFantasyLeagues(leagues);
        }

        public List<FantasyLeague> LoadPersistedFantasyLeagues()
        {
            return _Storage.LoadFantasyLeagues();
        }

    }
}
