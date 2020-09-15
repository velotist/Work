using System;

namespace Work
{

    /// <summary>Responsible for calculating the end time.</summary>
    public class CalculateWorkingTime : WorkLogging
    {
        private const int debitWorkingHours = 8;
        private const int debitWorkingMinutes = 0;
        private const string failureText = "Fehler...\n";

        /// <summary>Calculate the end time.</summary>
        /// <param name="hours">The hours in your start time as integer</param>
        /// <param name="minutes">The minutes in your start time as integer</param>
        /// <param name="pause">Duration of pause as integer</param>
        /// <returns>The end time in format HH:MM.</returns>
        public string EndTime(int hours, int minutes, int pause)
        {
            string endTime = "";

            int sumMinutes = minutes + debitWorkingMinutes + pause;

            if (sumMinutes < 10)
            {
                endTime = hours + debitWorkingHours + ":" + sumMinutes;
            }
            else if(sumMinutes >=10 && sumMinutes < 60)
            {
                endTime = hours + debitWorkingHours + ":0" + sumMinutes;
            }
            else
            {
                if (sumMinutes >= 60)
                {
                    int restMinutes = sumMinutes - 60;
                    if(restMinutes < 10)
                        endTime = (hours + debitWorkingHours + 1) + ":0" + restMinutes;
                    else
                        endTime = (hours + debitWorkingHours + 1) + ":" + restMinutes;
                }
                if (sumMinutes >= 120)
                {
                    endTime = (hours + debitWorkingHours + 2) + ":" + (sumMinutes - 120);
                }
            }

            return endTime;
        }

        /// <summary>Extracts the hours of a specified time.</summary>
        /// <param name="time">The start time.</param>
        /// <returns>The hours in your start time as integer.</returns>
        public int Hours(string time)
        {
            try
            {
                return Convert.ToInt32(time.Substring(0, 2));
            }
            catch (Exception ex)
            {
                LogException(failureText + ex.ToString());
                return -1;
            }
        }

        /// <summary>Extracts the minutes of a specified time.</summary>
        /// <param name="time">The start time.</param>
        /// <returns>The minutes in your start time as integer.</returns>
        public int Minutes(string time)
        {
            try
            {
                return Convert.ToInt32(time.Substring(3, 2));
            }
            catch (Exception ex)
            {
                LogException(failureText + ex.ToString());
                return -1;
            }
        }
    }
}