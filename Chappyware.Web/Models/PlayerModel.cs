using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chappyware.Web.Models
{
    public class PlayerStatsModel
    {
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Points { get; set; }
        public string Name { get; set; }
    }
}