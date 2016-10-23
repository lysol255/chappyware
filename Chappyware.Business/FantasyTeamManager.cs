using Chappyware.Data;
using Chappyware.Data.Factories;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Business
{
    public class FantasyTeamManager
    {

        public FantasyLeague CreateLeague(string name)
        {
            FantasyLeague league = new FantasyLeague();
            league.Name = name;
            return league;
        }

        public void UpdateLeagueRoster(FantasyLeague league, string fileToImport)
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
                string ownedStartDate = fields[2];
                string ownedEndDate = fields[3];

                // header marker, skip this row
                if (owner.Equals("Owner"))
                {
                    continue;
                }

                FantasyTeam team = teams.SingleOrDefault(t => t.Owner.Name == owner);
                if (team == null)
                {
                    team = new FantasyTeam();
                    team.Owner = new Owner(owner);
                    teams.Add(team);
                }

                FantasyPlayer player = new FantasyPlayer();
                player.Player = PlayerFactory.Instance.GetPlayerByName(playerName);

                if (string.IsNullOrEmpty(ownedStartDate))
                {
                    ownedStartDate = Season.GetSeasonStartDate("2016").ToString();
                }
                player.OwnedStartDate = Convert.ToDateTime(ownedStartDate);

                if (string.IsNullOrEmpty(ownedEndDate))
                {
                    ownedEndDate = Season.GetSeasonEndDate("2017").ToString();
                }
                player.OwnedEndDate = Convert.ToDateTime(ownedEndDate);

                team.OwnedPlayers.Add(player);

            }

            return teams;
        }
    }
}
