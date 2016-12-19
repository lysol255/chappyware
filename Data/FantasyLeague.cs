using Chappyware.Business;
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
            IStatSource csvSource = new HockeyReferenceDotComStatSource();
            csvSource.Initialize();

            List<Player> currentPlayerStats = StorageFactory.Instance.LoadPersistedStatSource();
            StatisticManager.UpdatePlayerStatistics(currentPlayerStats, csvSource);

            // persist them into json
            StorageFactory.Instance.UpdatedPersistedStatSource(currentPlayerStats);
        }

    }
}
