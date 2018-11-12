using Chappyware.Data;
using Chappyware.Data.Factories;
using Chappyware.Logging;
using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Data.DataObjects
{
    public class League
    {
        public string Name { get; set; }

        public string LeagueTeamFile { get; set; }

        public League()
        {

        }

        public List<FantasyTeam> GetTeams()
        {
            List<FantasyTeam> teams = new List<FantasyTeam>();
            try
            {
                CsvTextFieldParser parser = new CsvTextFieldParser(LeagueTeamFile);
                parser.Delimiters = new string[] { "," };

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
                        player = new Player(playerName, teamCode);
                    }

                    FantasyPlayer fantasyPlayer = new FantasyPlayer(player);

                    // assign the start date to the start of the season if not defiend
                    if (string.IsNullOrEmpty(ownedStartDate))
                    {
                        ownedStartDate = Season.GetSeasonStartDate().ToString();
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
            }
            catch(FileNotFoundException fnf)
            {
                Log.LogEvent($"Could not find file with name {LeagueTeamFile}");
            }
            return teams;
        }
    }
}
