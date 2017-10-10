using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chappyware.Web.Models
{
    public class TeamModel
    {
        public string OwnerName { get; set; }
        public List<PlayerModel> Players {get; set;}

        public TeamModel()
        {
            Players = new List<PlayerModel>();
        }

        public int TotalGoals
        {
            get
            {
                int totalGoals = 0;
                foreach (PlayerModel player in Players)
                {
                    totalGoals += player.Goals;
                }
                return totalGoals;
            }
        }

        public int TotalAssists
        {
            get
            {
                int totalAssits = 0;
                foreach (PlayerModel player in Players)
                {
                    totalAssits += player.Assists;
                }
                return totalAssits;
            }
        }

        public int TotalPoints
        {
            get
            {
                int totalPoints = 0;
                foreach (PlayerModel player in Players)
                {
                    totalPoints = totalPoints + player.Goals + player.Assists;
                }
                return totalPoints;
            }
        }

        public int TotalGamesPlayed
        {
            get
            {
                int totalGamesPlayed = 0;
                foreach(PlayerModel player in Players)
                {
                    totalGamesPlayed = totalGamesPlayed + player.GamesPlayed;
                }
                return totalGamesPlayed;
            }
        }

        public double TeamPointsPerGame
        {
            get
            {
                double ppg = 0;
                if(TotalGamesPlayed != 0)
                {
                    ppg = (double)TotalPoints / (double)TotalGamesPlayed;
                }
                return ppg;
            }
        }

        public int PointsBehindLeader { get; internal set; }
    }
}