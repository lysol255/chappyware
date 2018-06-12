using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Data.Factories
{
    public class PlayerFactory
    {
        private static PlayerFactory _Instance;
        private Dictionary<string, Statistic> _LeagueStats;

        public static PlayerFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PlayerFactory();
                }
                return _Instance;
            }
        }

        private PlayerFactory()
        {
            LeagueStatStore leagueStatsStore = new LeagueStatStore();
            leagueStatsStore.Load();

            _LeagueStats = leagueStatsStore.LeagueStats;

            if (_LeagueStats != null)
            {
                DateTime newestRecord = _LeagueStats.Values.Max(s => s.RecordDate);
                
                // update the newest stat record
                if (newestRecord < DateTime.Today)
                {
                    //UpdateLeagueStore(leagueStatsStore);

                }
            }
            else
            {
                UpdateLeagueStore(leagueStatsStore);
            }

        }

        private void UpdateLeagueStore(LeagueStatStore leagueStatsStore)
        {
            HockeyReferenceDotComStatSource leagueStats = new HockeyReferenceDotComStatSource();
            leagueStats.Initialize();
            _LeagueStats = leagueStats.LoadLeagueStats();
            leagueStatsStore.LeagueStats = _LeagueStats;
            leagueStatsStore.Save();
        }

        public Player GetPlayer(string playerName, string teamCode)
        {
            GameStatFactory manager = GameStatFactory.Instance;

            //TODO handle duplicates, maybe not an issue
            Player foundPlayer = new Player();
            foundPlayer.Name = playerName;
            foundPlayer.Team = teamCode;

            foundPlayer.GameStats = manager.GetPlayerStatCollection(playerName, teamCode);
            if (_LeagueStats.ContainsKey(GetPlayerGuid(foundPlayer)))
            {
                foundPlayer.Stats.Add(_LeagueStats[GetPlayerGuid(foundPlayer)]);
            }
            return foundPlayer;
        }

        public static string GetPlayerGuid(Player player)
        {
            return player.Name + "_" + player.Team;
        }
    }
}
