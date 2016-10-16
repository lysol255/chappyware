using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chappyware.Data;

namespace Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            IStatSource csvSource = new HockeyReferenceDotComStatSource();
            csvSource.Initialize();
            List<Player> players = csvSource.LoadPlayers();

        }
    }
}
