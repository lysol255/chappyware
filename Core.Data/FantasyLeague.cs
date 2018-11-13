using System;
using System.Collections.Generic;

namespace Core.Data
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
