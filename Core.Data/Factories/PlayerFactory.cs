using Chappyware.Data.Storage;
using Core.Data.DataObjects;
using Core.Data.Storage;
using System.Collections.Generic;

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

        }

        private void UpdateLeagueStore(LeagueStatStore leagueStatsStore)
        {

        }

        public Player GetPlayer(string playerName, string teamCode)
        {
            PlayerStore playerStore = new PlayerStore();
            Player searchPlayer = new Player(playerName, teamCode);
            searchPlayer = playerStore.ReadPlayerRecord(searchPlayer.Id);
            return searchPlayer;
        }

        public void UpdatePlayer(Player player)
        {
            PlayerStore playerStore = new PlayerStore();
            playerStore.UpdatePlayerRecord(player);
        }

        public Player CreatePlayer(string name, string teamCode)
        {
            Player p = new Player(name, teamCode);
            return p;
        }
    }
}
