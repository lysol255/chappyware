using Chappyware.Data;
using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Business
{
    public class StatisticManager
    {
        public List<Player> GetHistorialPlayerStatistics()
        {
            List<Player> currentPlayers = StorageFactory.Instance.LoadPersistedStatSource();
            return currentPlayers;
        }

        public static void UpdatePlayerStatistics(List<Player> currentPlayerStats, IStatSource newStats)
        {
            throw new NotImplementedException();
        }

    }
}
