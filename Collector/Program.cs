using Chappyware.Data.DataSources;
using Chappyware.Data.Storage;
using System.Collections.Generic;

namespace Collector
{
    class Program
    {
        static void Main(string[] args)
        {

            HockeyReferenceGameStatSource source = new HockeyReferenceGameStatSource(new Dictionary<string, GameStats>());
            Dictionary<string, GameStats> stats = source.UpdateHistoricalStats();

            GameStatStore store = new GameStatStore();
            store.HistoricalGames = stats;

            store.Save();

            stats = null;

            store.Load();

            stats = store.HistoricalGames;

            /*
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


            FantasyTeamManager manager = FantasyTeamManager.Insance;
            FantasyLeague league = manager.GetLeague("Robs");
            manager.InsertTeamsIntoLeague(league, "TeamImport.txt");
            */

        }
    }
}
