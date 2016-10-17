using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chappyware.Data;
using Chappyware.Data.Storage;

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
            StatisticManager.UpdatePlayerStatistics(currentPlayerStats, csvSource);

            // persist them into json
            StorageFactory.Instance.UpdatedPersistedStatSource(currentPlayerStats);

        }
    }
}
