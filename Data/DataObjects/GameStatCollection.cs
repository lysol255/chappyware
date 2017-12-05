using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data.DataObjects
{
    public class GameStatCollection
    {        

        public List<GameStat> GameStats { get; }

        public GameStatCollection(List<GameStat> gameStats)
        {
            GameStats = gameStats;
        }

        public PlayerGameStatCollection GetPlayerGameCollection(string playerName, string teamCode)
        {
            PlayerGameStatCollection playerGameCollection = new PlayerGameStatCollection();

            var teamGameCollection = GameStats.Where(g => g.AwayTeamCode.Equals(teamCode) || g.HomeTeamCode.Equals(teamCode));

            foreach(GameStat game in teamGameCollection)
            {
                PlayerGameStat playerGameStat = game.GetPlayerGameStat(playerName, teamCode);
                if (playerGameStat != null)
                {
                    playerGameCollection.PlayerStats.Add(playerGameStat);
                    playerGameStat.GameDate = game.GetGameDate();
                }
            }

            return playerGameCollection;
        }
    }
}
