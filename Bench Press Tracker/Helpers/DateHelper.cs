using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench_Press_Tracker.Helpers
{
    public class DateHelper
    {
        public static string calculateDuration(string startTime, string endTime)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            return (end - start).ToString();
        }

        public static string getUserDate()
        {
            while (true)
            {
                Console.WriteLine("Please enter the date in the following format (dd-MM-yy HH:mm)");
                string? date = Console.ReadLine();
                string format = "dd-MM-yy HH:mm";
                DateTime dateValue;
                bool isValidDate = DateTime.TryParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                    out dateValue);
                if (isValidDate)
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("Please enter the date in the correct format!");
                }
            }
        }
    }
}
