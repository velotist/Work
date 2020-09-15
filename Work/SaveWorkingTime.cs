using System;
using System.IO;

namespace Work
{

    /// <summary>Responsible for saving data to a file.</summary>
    public class SaveWorkingTime : WorkLogging
    {
        /// <summary>Saves to file.</summary>
        /// <param name="storageLocation">The storage location.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="pause">Duration of pause.</param>
        public void SaveToFile(string storageLocation, string startTime, string endTime, int pause)
        {
            try
            {
                StreamWriter file;

                if (File.Exists(storageLocation)) {
                    file = new StreamWriter(storageLocation, true);
                }
                else
                {
                    file = new StreamWriter(storageLocation);
                }

                if(pause == 1)
                    file.WriteLine(DateTime.Now.ToLongDateString() + "   Beginn: " + startTime + " Ende: " + endTime + " Pausendauer: " + pause + " Minute");
                else
                    file.WriteLine(DateTime.Now.ToLongDateString() + "   Beginn: " + startTime + " Ende: " + endTime + " Pausendauer: " + pause + " Minuten");

                Console.WriteLine();
                Console.WriteLine("Arbeitszeiten gespeichert unter " + storageLocation);

                file.Close();
            }
            catch (Exception ex)
            {
                LogException("Pfad bzw. Datei nicht gefunden...\n" + ex.ToString());
            }
        }
    }
}