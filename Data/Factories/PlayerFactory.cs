using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            JsonStorage store = new JsonStorage();
            Player returnedPlayer = store.Get
        }

        public void CreatePlayer(string playerName, string teamCode, int age)
        {
            Player newPlayer = new Player(playerName, teamCode, age);

            if (!Exists(playerName, teamCode, age))
            {
                newPlayer.Id = Guid.NewGuid().ToString();
            }

            //TODO handle duplicates, maybe not an issue
            Player foundPlayer = new Player();
            foundPlayer.Name = playerName;
            foundPlayer.Team = teamCode;

            foundPlayer.GameStats = manager.GetPlayerStatCollection(playerName, teamCode); 
            return foundPlayer;
        }

    }
}
