using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chappyware.Web.Models
{
    public class TeamModel
    {
        public string OwnerName { get; set; }
        public List<PlayerStatsModel> Players {get; set;}

        public TeamModel()
        {
            Players = new List<PlayerStatsModel>();
        }

        public int TotalGoals
        {
            get
            {
                int totalGoals = 0;
                foreach (PlayerStatsModel player in Players)
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
                foreach (PlayerStatsModel player in Players)
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
                foreach (PlayerStatsModel player in Players)
                {
                    totalPoints = totalPoints + player.Goals + player.Assists;
                }
                return totalPoints;
            }
        }

    }
}