using System;

namespace NewsApi.Utils.TimeUtils
{
    /// <summary>
    /// A static class for bulding dates.
    /// </summary>
    public static class DateBuilder
    {
        /// <summary>
        /// Tries to convert given parameters to a DateTime instance.
        /// </summary>
        /// <param name="year">A year as a string</param>
        /// <param name="month">A month as a string</param>
        /// <param name="day">A day as a string</param>
        /// <returns>The corresponding nullable DateTime if succesful, null otherwise</returns>
        public static DateTime? CreateDate(string year, string month, string day)
        {
            int y = 0, m = 0, d = 0;
            if (Int32.TryParse(year, out y) && Int32.TryParse(month, out m) && Int32.TryParse(day, out d))
            {
                try 
                {
                    var date = new DateTime(y, m, d);
                    return date;
                }
                catch (ArgumentOutOfRangeException) 
                { 
                    // returns null
                }
            }
            return null;
        }
    }
}