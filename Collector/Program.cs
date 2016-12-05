using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chappyware.Data;
using Chappyware.Data.Storage;
using Chappyware.Business;

namespace Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            // get the current stats and load them into memory
            IStatSource csvSource = new HockeyReferenceDotComStatSource();
            csvSource.Initialize();

            List<Player> currentPlayerStats = StorageFactory.Instance.LoadPersistedStatSource();

            foreach(Player player in currentPlayerStats)
            {
                foreach(Statistic stat in player.Stats)
                {
                    stat.RecordDate = stat.RecordDate.AddHours(stat.RecordDate.Hour * -1);
                    stat.RecordDate = stat.RecordDate.AddMinutes(stat.RecordDate.Minute * -1);
                    stat.RecordDate = stat.RecordDate.AddSeconds(stat.RecordDate.Second * -1);
                    stat.RecordDate = stat.RecordDate.AddMilliseconds(stat.RecordDate.Millisecond* -1);
                    
                    stat.RecordDate = stat.RecordDate.ToUniversalTime();
                }
            }


            //// persist them into json
            StorageFactory.Instance.UpdatedPersistedStatSource(currentPlayerStats);

            StatisticManager.UpdatePlayerStatistics(currentPlayerStats, csvSource);

            
            FantasyTeamManager manager = new FantasyTeamManager();
            FantasyLeague league = manager.CreateLeague("Robs");
            manager.UpdateLeagueRoster(league, "TeamImport.txt");

        }
    }
}
