using Chappyware.Data.DataObjects;
using System.Collections.Generic;

namespace Chappyware.Data
{
    public interface IStatStorage
    {
        List<Player> LoadPlayers();

        void SavePlayers(List<Player> players);

        List<FantasyLeague> LoadFantasyLeagues();

        void SaveFantasyLeagues(List<FantasyLeague> fantasyLeagues);

        Dictionary<string, GameStat> LoadGameStats();

        // CRUD operations
        void CreateGameStat(GameStat gameStat);
        GameStat ReadGameStat(string url);
        void UpdateGameStat(GameStat gameStat);
        void DeleteGameStat(GameStat gameStat);


    }
}
