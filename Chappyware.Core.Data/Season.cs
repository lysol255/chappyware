using System;

namespace Chappyware.Data
{
    public class Season
    {

        public const int CURRENT_SEASON_YEAR = 2017;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetSeasonStartDate(int year)
        {
            DateTime seasonStart = DateTime.MinValue;
            if (year == 2017)
            {
                seasonStart = new DateTime(2017, 10, 01);
            }

            return seasonStart;
        }

        public static DateTime GetSeasonEndDate(int year)
        {

            DateTime seasonEnd = DateTime.MinValue;
            if (year == 2017)
            {
                seasonEnd = new DateTime(2018, 05, 04);
            }

            return seasonEnd;

        }
    }
}
