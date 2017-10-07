using Chappyware.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public interface IStatStorage
    {
        List<Player> LoadPlayers();

        void SavePlayers(List<Player> players);

        List<FantasyLeague> LoadFantasyLeagues();

        void SaveFantasyLeagues(List<FantasyLeague> fantasyLeagues);

        Dictionary<string, GameStat> LoadGameStats();
    }
}
