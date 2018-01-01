using System;

namespace NewsApi.Utils.TimeUtils
{
    public static class DateBuilder
    {
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
                    
                }
            }
            return null;
        }
    }
}