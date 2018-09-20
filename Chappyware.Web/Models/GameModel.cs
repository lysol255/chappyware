using System;

namespace Chappyware.Web.Models
{
    public class GameModel
    {
        public DateTime RecordDate { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeam { get; set; }
        public int AwayGoals { get; set; }
        public int HomeGoals { get; set; }
    }
}