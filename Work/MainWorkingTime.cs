using System.Configuration;
using System;

namespace Work
{
    /// <summary>Implements Logging class.</summary>
    /// <seealso cref="Work.WorkLogging" />
    public class MainWorkingTime : WorkLogging
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException">args</exception>
        static void Main(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            CalculateWorkingTime firstCashWorkingTime = new CalculateWorkingTime();
            SaveWorkingTime firstCash = new SaveWorkingTime();

            string directory = ConfigurationManager.AppSettings["directory"];
            string fileName = ConfigurationManager.AppSettings["fileName"];

            string startTime = "";
            int pause = 0;
            bool status = true;

            LogInfo("Starte Applikation...");

            /*
             *  Behandle die Eingabe des Arbeitsbeginns, das heißt prüfe auch auf Plausibilität und Ausnahmefälle
             */
            try
            {
                while (status) {
                    Console.Write("Arbeitbeginn (HH:MM):    ");
                    startTime = Console.ReadLine();

                    if (string.IsNullOrEmpty(startTime) || startTime.Length <= 2 && !startTime.Equals("q"))
                    {
                        Console.WriteLine("Bitte Arbeitsbeginn eingeben.");
                    }
                    else if (startTime.Equals("q"))
                    {
                        Console.WriteLine();
                        LogInfo("Beende Applikation.");
                        Console.WriteLine();
                        Console.WriteLine("Press a key...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else if (startTime.Substring(2, 1) != ":" || startTime.Length <= 3 || startTime.Length > 5)
                    {
                        Console.WriteLine("Format HH:MM beachten (z.B. 08:04)");
                    }
                    else if (Convert.ToInt32(startTime.Substring(0, 2)) < 7 || Convert.ToInt32(startTime.Substring(0, 2)) > 17)
                    {
                        Console.WriteLine("Bitte die Stunden im Bereich von 7 bis 17 eingeben");
                    }
                    else if (Convert.ToInt32(startTime.Substring(3, 2)) < 0 || Convert.ToInt32(startTime.Substring(3, 2)) > 59)
                    {
                        Console.WriteLine("Bitte die Minuten im Bereich von 0 bis 59 eingeben");
                    }
                    
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException("Fehler...\n" + ex.ToString());
            }

            /*
             *  Behandle die Eingabe der Pausendauer, das heißt prüfe auf Plausibilität und Ausnahmefälle
             */
            bool flag = true;

            do
            {
                try
                {
                    Console.Write("Pausendauer in Minuten:  ");
                    pause = int.Parse(Console.ReadLine());

                    if(pause < 0 || pause > 120)
                    {
                        Console.WriteLine("Bitte die Minuten im Bereich von 0 bis 120 eingeben.");
                    }
                    else
                        flag = false;
                }
                catch (FormatException)
                {
                    LogException("Ungültige oder keine Zahl.");
                }
                catch(OverflowException)
                {
                    LogException("Die Zahl liegt nicht im Bereich.");
                }
            } while (flag);

            Console.WriteLine();
            /*
             *  Zerlege den String des eingegebenen Arbeitsbeginns in Stunden und Minuten und übergebe diese Werte mitsamt der Pausendauer der Methode EndTime(), die die Endzeit berechnet. Es wird ein konkatenierter String aus Text und Rückgabewert der Methode EndTime() ausgegeben
             */
            Console.WriteLine("Die Arbeitszeit endet um " +
                firstCashWorkingTime.EndTime(firstCashWorkingTime.Hours(startTime), firstCashWorkingTime.Minutes(startTime), pause) +
                " Uhr.");

            firstCash.SaveToFile(directory + fileName,
                startTime,
                firstCashWorkingTime.EndTime(firstCashWorkingTime.Hours(startTime), firstCashWorkingTime.Minutes(startTime), pause), pause);

            LogInfo("Beende Applikation.");

            Console.WriteLine("Press a key...");
            Console.ReadKey();
        } 
    }
}