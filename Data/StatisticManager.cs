using Chappyware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Business
{
    public class StatisticManager
    {

        public static void UpdatePlayerStatistics(List<Player> currentPlayerStats, IStatSource newStats)
        {
            // determine if the new stats have already been recorded today
            Player newStatSourcePlayer = newStats.LoadPlayers().FirstOrDefault();
            Player existingStatSourcePlayer = currentPlayerStats.FirstOrDefault();

            if (newStatSourcePlayer != null
                && existingStatSourcePlayer != null)
            {
                // if the new stats have the same day as the most recent exisitng stat source
                // they have already been recorded
                Statistics stats = newStatSourcePlayer.Stats.FirstOrDefault();
                DateTime latestStatRecordDate = existingStatSourcePlayer.Stats.Max(s => s.RecordDate);

                if (stats != null
                    && stats.RecordDate.DayOfYear == latestStatRecordDate.DayOfYear
                    && stats.RecordDate.Year == latestStatRecordDate.Year)
                {
                    return;
                }
            }

            foreach (Player player in newStats.LoadPlayers())
            {
                // find the player if it already has stats saved
                Player currentPlayer = currentPlayerStats.SingleOrDefault(p => p.Name.Equals(player.Name));

                // if not create a new player and add it to the list
                if (currentPlayer == null)
                {
                    currentPlayer = new Player();
                    currentPlayerStats.Add(currentPlayer);
                }

                // update the player's name and team
                currentPlayer.Name = player.Name;
                currentPlayer.Team = player.Team;

                // add the new statistics to the player, the new stats will only have one entry
                // TODO only add the stat if the record isn't from the same day
                currentPlayer.Stats.Add(player.Stats.First());
            }
        }

    }
}
