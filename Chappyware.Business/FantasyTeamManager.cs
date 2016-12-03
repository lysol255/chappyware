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
                string teamName = string.Empty;

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

                // player and team name are included in the same cell
                string[] playerAndTeam = playerName.Split(',');
                if (playerAndTeam.Length == 2)
                {
                    playerName = playerAndTeam[0].Trim();
                    teamName = playerAndTeam[1].Trim();
                }

                // find the player
                Player player = PlayerFactory.Instance.GetPlayer(playerName, teamName);

                // create empty player if the player has no points/can't be found
                if (player == null)
                {
                    player = new Player();
                    player.Name = playerName;
                    player.Stats = new List<Statistic>();
                    player.Team = teamName;
                }

                FantasyPlayer fantasyPlayer = new FantasyPlayer(player);

                // assign the start date to the start of the season if not defiend
                if (string.IsNullOrEmpty(ownedStartDate))
                {
                    ownedStartDate = Season.GetSeasonStartDate("2016").ToString();
                }
                fantasyPlayer.OwnedStartDate = Convert.ToDateTime(ownedStartDate);

                // assign the end date to the end of the season if not defined
                if (string.IsNullOrEmpty(ownedEndDate))
                {
                    ownedEndDate = Season.GetSeasonEndDate("2017").ToString();
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
