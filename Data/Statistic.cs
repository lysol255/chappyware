﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Data
{
    public class Statistic
    {
        public int Goals { get; set; }
        public int GamesPlayed { get; set; }
        public int Assists { get; set; }

        // In UTC
        public DateTime RecordDate { get; set; }
        public string AvgTOI { get; set; }
    }
}
