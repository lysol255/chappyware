using Chappyware.Data;
using Chappyware.Data.Factories;
using Chappyware.Data.Storage;
using Microsoft.VisualBasic.FileIO;
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
                InsertTeamsIntoLeague(league, DataFileUtilities.GetLeagueFileName());
                _LeagueCache.Add(league);
            }
            return league;
        }

        public void ResetLeagueCache(string leagueName)
        {
            _LeagueCache.RemoveAll(l => l.Name == leagueName);
        }

        public void InsertTeamsIntoLeague(FantasyLeague league, string fileToImport)
        {
            league.Teams = ImportTeamFromCsv(fileToImport);
        }

        private List<FantasyTeam> ImportTeamFromCsv(string fileToImport)
        {
            TextFieldParser parser = new TextFieldParser(fileToImport);
            parser.Delimiters = new string[] { "," };

            List<FantasyTeam> teams = new List<FantasyTeam>();

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                string owner = fields[0];
                string playerName = fields[1];
                string teamCode = fields[2];
                string ownedStartDate = fields[3];
                string ownedEndDate = fields[4];

                // header marker, skip this row
                if (owner.Equals("Owner"))
                {
                    continue;
                }

                // find the fantasy team and create it otherwise
                FantasyTeam team = teams.SingleOrDefault(t => t.Owner.Name == owner);
                if (team == null)
                {
                    team = new FantasyTeam();
                    team.Owner = new Owner(owner);
                    teams.Add(team);
                }

                // find the player
                Player player = PlayerFactory.Instance.GetPlayer(playerName, teamCode);

                // create empty player if the player has no points/can't be found
                if (player == null)
                {
                    player = new Player();
                    player.Name = playerName;
                    player.CurrentTeam = teamCode;
                }

                FantasyPlayer fantasyPlayer = new FantasyPlayer(player);

                // assign the start date to the start of the season if not defiend
                if (string.IsNullOrEmpty(ownedStartDate))
                {
                    ownedStartDate = Season.GetSeasonStartDate(Season.CURRENT_SEASON_YEAR).ToString();
                }
                fantasyPlayer.OwnedStartDate = Convert.ToDateTime(ownedStartDate);

                // assign the end date to the end of the season if not defined
                if (string.IsNullOrEmpty(ownedEndDate))
                {
                    ownedEndDate = Season.GetSeasonEndDate(Season.CURRENT_SEASON_YEAR).ToString();
                }
                fantasyPlayer.OwnedEndDate = Convert.ToDateTime(ownedEndDate);

                // assign the draft round
                fantasyPlayer.DraftRound = team.OwnedPlayers.Count + 1;

                // add it to the list of owned players
                team.OwnedPlayers.Add(fantasyPlayer);

            }

            return teams;
        }
    }
}
