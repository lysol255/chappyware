using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Business
{
    class Season
    {
        public static DateTime GetSeasonStartDate(string year)
        {
            return new DateTime(2016, 10, 08);
        }

        public static DateTime GetSeasonEndDate(string year)
        {
            return new DateTime(2017, 04, 04);
        }
    }
}
