using Chappyware.Data;
using Chappyware.Data.Factories;
using Chappyware.Data.Storage;
using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chappyware.Business
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
            throw new NotImplementedException();
        }

        private FantasyTeamManager()
        {
            _LeagueCache = new List<FantasyLeague>();
        }

        private FantasyLeague CreateLeague(string name)
        {
            FantasyLeague league = new FantasyLeague();
            league.Name = name;
            return league;
        }

        public FantasyLeague GetLeague(string leagueName)
        {
            FantasyLeague league = _LeagueCache.SingleOrDefault(l=>l.Name == leagueName);

            if (league == null)
            {
                league = CreateLeague(leagueName);
                _LeagueCache.Add(league);
            }
            return league;
        }

        public void ResetLeagueCache(string leagueName)
        {
            _LeagueCache.RemoveAll(l => l.Name == leagueName);
        }
       
    }
}
