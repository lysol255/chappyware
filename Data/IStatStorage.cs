using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public interface IStatStorage
    {
        List<Player> Load();

        void Save(List<Player> players);
    }
}
