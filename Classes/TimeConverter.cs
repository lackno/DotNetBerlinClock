using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            // The input is presumed correct, so no checks for valid values and numbers in valid range are done.
            // The below code only works with this assumption, so for a real case scenario checks and messages returned to the user should be implemented here.
            // The 3 parts of the clock are separate problems so we can split them for readability.
            int hours = Convert.ToInt32(aTime.Split(':')[0]);
            int minutes = Convert.ToInt32(aTime.Split(':')[1]);
            int seconds = Convert.ToInt32(aTime.Split(':')[2]);

            // The return string with the lights values can be build from top to bottom
            // First row is simply the odd-even value of the seconds field
            string clockLights = seconds % 2 == 0 ? "Y" : "O";
            clockLights += Environment.NewLine;

            // For the first hours row, we need to know how many blocks of 5-hours it can contain
            int nr5HoursBlock = hours / 5;
            clockLights += new String('R', nr5HoursBlock) + new String('O', 4 - nr5HoursBlock) + Environment.NewLine; // Where 4 is the maximum number of lights in the row
            // For the second hours row, we need the rest of the previous operation
            int leftSingleHours = hours % 5;
            clockLights += new String('R', leftSingleHours) + new String('O', 4 - leftSingleHours) + Environment.NewLine;

            // For the minutes the same logic applies, but with some differences in the first row
            // Each third Y in a row becomes R, and the maximum number of lights is 11
            int nr5MinutesBlock = minutes / 5;
            clockLights += new String('Y', nr5MinutesBlock).Replace("YYY", "YYR") + new String('O', 11 - nr5MinutesBlock) + Environment.NewLine;
            // The second row of minutes works in the same way as the second row of hours
            int leftSingleMinutes = minutes % 5;
            clockLights += new String('Y', leftSingleMinutes) + new String('O', 4 - leftSingleMinutes);
            
            return clockLights;
            /* NOTE: If a similar clock with different values or colors should need to be implemented, all colors (Y R O),
               size of blocks (5-hours, 5-minutes) and length of rows (4, 11) should be passed as parameter.
               For this unique fixed case they are directly in the code. */
        }
    }
}
