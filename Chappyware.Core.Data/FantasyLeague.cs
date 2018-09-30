using Chappyware.Data.DataObjects;
using Chappyware.Data.DataSources;
using Chappyware.Data.Storage;
using System;
using System.Collections.Generic;

namespace Chappyware.Data
{
    public class FantasyLeague
    {
        public string Name { get; set; }

        public List<FantasyTeam> Teams { get; set; }

        public FantasyLeague()
        {
            Teams = new List<FantasyTeam>();
        }

        public void UpdateLeague()
        {
            throw new NotImplementedException();

        }

    }
}
