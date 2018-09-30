using System.Collections.Generic;

namespace Chappyware.Data
{
    public interface IStatSource
    {
        void Initialize();

        List<Player> LoadPlayers();

        List<Team> LoadTeams();
    }
}
