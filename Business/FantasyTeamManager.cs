using Chappyware.Logging;
using Core.Data.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class FantasyTeamManager
    {

        private static FantasyTeamManager _Instance;
                
        private List<FantasyLeague> _LeagueCache;

        public static FantasyTeamManager Insance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FantasyTeamManager();
                }
                return _Instance;
            }
        }

        public List<FantasyTeam> GetTeamsForLeague(string leagueName)
        {
            LeagueStore store = new LeagueStore();
            League loadedLeague = store.ReadLeague(leagueName);

            if (loadedLeague == null)
            {
                Log.LogEvent($"League {leagueName} could not be loaded.  Returning an empty league.");
                loadedLeague = new League();
                loadedLeague.Name = "unset";
            }

            return loadedLeague.GetTeams();
        }

        private FantasyTeamManager()
        {
            _LeagueCache = new List<FantasyLeague>();
        }
               
       
    }
}
