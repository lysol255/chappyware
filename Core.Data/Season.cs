using System;

namespace Chappyware.Data
{
    public class Season
    {

        public const int CURRENT_SEASON_YEAR = 2018;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetSeasonStartDate()
        {
            DateTime seasonStart = DateTime.MinValue;
            seasonStart = new DateTime(CURRENT_SEASON_YEAR, 10, 01);
            return seasonStart;
        }

        public static DateTime GetSeasonEndDate(int year)
        {
            DateTime seasonEnd = DateTime.MinValue;
            seasonEnd = new DateTime(CURRENT_SEASON_YEAR, 05, 04);
            return seasonEnd;

        }
    }
}
