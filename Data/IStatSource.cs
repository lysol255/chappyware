using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public interface IStatSource
    {
        void Initialize();

        List<Player> LoadPlayers();

        List<Team> LoadTeams();
    }
}
