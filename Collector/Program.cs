using Chappyware.Business;
using Chappyware.Data;
using Chappyware.Data.DataObjects;
using Chappyware.Data.DataSources;
using Chappyware.Data.Factories;
using Chappyware.Data.Storage;
using System.Collections.Generic;
using System.IO;

namespace Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerRecordStore store = new PlayerRecordStore();
            store.Load();

            //store.HistoricalGames.Remove("http://www.hockey-reference.com/boxscores/201710050OTT.html");

            if (store.HistoricalGames == null)
            {
                store.HistoricalGames = new Dictionary<string, GameStat>();
            }

            HockeyReferenceGameStatSource source = new HockeyReferenceGameStatSource(store.HistoricalGames);
            Dictionary<string, GameStat> stats = source.UpdateHistoricalStats();

            store.HistoricalGames = stats;

            store.Save();

            string[] games = new string[store.HistoricalGames.Keys.Count];

            store.HistoricalGames.Keys.CopyTo(games,0);

            foreach (string game in games)
            {
                if (store.HistoricalGames[game].AwayTeamCode == null
                    ||
                    store.HistoricalGames[game].HomeTeamCode == null)
                {
                    store.HistoricalGames.Remove(game);

                    if (!File.Exists(DataFileUtilities.GetLogFileName()))
                    {
                        File.CreateText(DataFileUtilities.GetLogFileName());
                    }

                    string log = File.ReadAllText(DataFileUtilities.GetLogFileName());
                    log = log + "\n" + "Error loading game, no away or home team : " + game;
                    File.WriteAllText(DataFileUtilities.GetLogFileName(), log);
                }
            }

            store.Save();

            stats = null;
            
            stats = store.HistoricalGames;

            PlayerFactory playerGameStatManager = PlayerFactory.Instance;

            Player p = playerGameStatManager.GetPlayer("Connor McDavid", "EDM");

            FantasyTeamManager teamManager = FantasyTeamManager.Insance;
            FantasyLeague league = teamManager.GetLeague("Robs");


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
