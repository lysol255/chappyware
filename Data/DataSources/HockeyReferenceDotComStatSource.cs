using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Chappyware.Data
{
    public class HockeyReferenceDotComStatSource : IStatSource
    {
        private List<Player> _Players;

        public void Initialize()
        {

            try
            {
                // create a list of players
                _Players = new List<Player>();

                // configure the request to go to hockey reference
                HttpWebRequest request = WebRequest.Create("http://www.hockey-reference.com/leagues/NHL_2017_skaters.html") as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                // read the html as a string
                string responseText = null;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    while(!reader.EndOfStream)
                    {
                        responseText += reader.ReadLine().Trim();
                    }
                }

                // find the player table
                Regex dataTableSearch = new Regex("<table class=\"sortable stats_table\" id=\"stats\".*</table>");
                Match dataTable = dataTableSearch.Match(responseText);

                // match each player row into a collection of matches
                Regex playerRowSearch = new Regex("<tr >.*?</tr>");
                MatchCollection playersCollection = playerRowSearch.Matches(dataTable.Value);

                DateTime initalzationDate = DateTime.Now;

                foreach(Match playerMatch in playersCollection)
                {
                    // parse out the stats
                    //< td class="right " data-stat="time_on_ice" >46</td>
                    Regex playerStatSearch = new Regex("<td .*?</td>");
                    MatchCollection playerStats = playerStatSearch.Matches(playerMatch.Value);

                    Player newPlayer = new Player();
                    Statistic newStats = new Statistic();
                    newStats.RecordDate = initalzationDate;

                    foreach(Match playerStatMatch in playerStats)
                    {
                        // read out the name and value of a stat
                        XDocument statDoc = XDocument.Parse(playerStatMatch.Value);
                        string statName = statDoc.Root.Attribute("data-stat").Value;
                        string statValue = statDoc.Root.Value;

                        // handle assigning the stat
                        ProcessStat(statName, statValue, newPlayer, newStats);

                    }
                    newPlayer.Stats.Add(newStats);
                    _Players.Add(newPlayer);
                }


            }
            catch(Exception e)
            {

            }

        }

        private void ProcessStat(string statName, string statValue, Player player,  Statistic statistics)
        {
            switch(statName)
            {
                case "player":
                    player.Name = statValue;
                    break;
                case "goals":
                    statistics.Goals = int.Parse(statValue);
                    break;
                case "assists":
                    statistics.Assists = int.Parse(statValue);
                    break;
                case "games_played":
                    statistics.GamesPlayed = int.Parse(statValue);
                    break;
                case "time_on_ice_avg":
                    statistics.AvgTOI = statValue;
                    break;
                case "team_id":
                    player.Team = statValue;
                    break;
            }
        }

        public List<Player> LoadPlayers()
        {
            return _Players;
        }

        public List<Team> LoadTeams()
        {
            throw new NotImplementedException();
        }
    }
}
