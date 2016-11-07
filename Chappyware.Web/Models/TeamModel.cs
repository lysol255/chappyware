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
    }
}