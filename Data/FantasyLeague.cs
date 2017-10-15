using Chappyware.Business;
using Chappyware.Data.DataObjects;
using Chappyware.Data.DataSources;
using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class FantasyLeague
    {
        public string Name { get; set; }

        public List<FantasyTeam> Teams { get; set; }

        public FantasyLeague()
        {
            Teams = new List<FantasyTeam>();
        }

        public void UpdateLeague()
        {
            // get the current stats and load them into memory
            GameStatStore store = new GameStatStore();
            store.Load();

            if (store.HistoricalGames == null)
            {
                store.HistoricalGames = new Dictionary<string, GameStat>();
            }

            HockeyReferenceGameStatSource source = new HockeyReferenceGameStatSource(store.HistoricalGames);
            Dictionary<string, GameStat> stats = source.UpdateHistoricalStats();

            store.HistoricalGames = stats;

            store.Save();

        }

    }
}
