using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class PuckAnalyticsCsvDataSource : IStatSource
    {
        private string _File;
        private List<Player> _Players;

        private int NAME_INDEX = 0;
        private int TEAM_INDEX = 1;
        private int GP_INDEX = 3;
        private int GF_INDEX = 5;
        private int GOALS_INDEX = 21;
        private int ASSISTS_INDEX = 22;
        private int POINTS_INDEX = 24;
        private int SHOTS_INDEX = 26;
        private int FENWICK_INDEX = 27;
        private int CORSI_INDEX = 28;

        public void Initialize()
        {
            _Players = new List<Player>();
            _File = @"E:\Dev\vs\chappyware\chappyware\Collector\Collector\SuperSkaterStats2015.csv";
        }

        //http://puckalytics.com/skatersuperstats.html
        public List<Player> LoadPlayers()
        {
            TextFieldParser parser = new TextFieldParser(_File);
            parser.Delimiters = new string[] { "," };

            // Skip over header line.
            if (!ValidHeader(parser.ReadFields())) throw new Exception();
            
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields.Length != 43) break;
                Player player = CreatePlayer(fields);
                _Players.Add(player);
            }
            return _Players;
        }

        private Player CreatePlayer(string[] fields)
        {
            Player player = new Player();

            player.Name = fields[NAME_INDEX];
            player.Team = fields[TEAM_INDEX];
            player.GamesPlayed = Convert.ToInt32(fields[GP_INDEX]);
            player.GoalsFor = Convert.ToInt32(fields[GF_INDEX]);
            player.Goals = Convert.ToInt32(fields[GOALS_INDEX]);
            player.Assists = Convert.ToInt32(fields[ASSISTS_INDEX]);
            player.Shots = Convert.ToInt32(fields[SHOTS_INDEX]);
            player.Fenwick = Convert.ToInt32(fields[FENWICK_INDEX]);
            player.Corsi = Convert.ToInt32(fields[CORSI_INDEX]);

            return player;
        }

        private bool ValidHeader(string[] header)
        {
            bool valid = true;
            if (!header[NAME_INDEX].Contains("Player Name")) valid = false;
            if (!header[TEAM_INDEX].Contains("Team")) valid = false;
            if (!header[GP_INDEX].Contains("GP")) valid = false;
            if (!header[GF_INDEX].Contains("GF")) valid = false;
            if (!header[GOALS_INDEX].Contains("iGoals")) valid = false;
            if (!header[ASSISTS_INDEX].Contains("iAssists")) valid = false;
            if (!header[POINTS_INDEX].Contains("iPoints")) valid = false;
            if (!header[SHOTS_INDEX].Contains("iShots")) valid = false;
            if (!header[FENWICK_INDEX].Contains("iFenwick")) valid = false;
            if (!header[CORSI_INDEX].Contains("iCorsi")) valid = false;
            return valid;
        }

        public List<Team> LoadTeams()
        {
            throw new NotImplementedException();
        }
    }
}
