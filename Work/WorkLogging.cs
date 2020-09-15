using NLog;
using System;

namespace Work
{
    /// <summary>Responsible for logging.</summary>
    public abstract class WorkLogging
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>Logs the information.</summary>
        /// <param name="text">The information message.</param>
        public static void LogInfo(string text)
        {
            Console.WriteLine();
            logger.Info(text);
            Console.WriteLine();
        }

        /// <summary>Logs the exception.</summary>
        /// <param name="text">The text.</param>
        public static void LogException(string text)
        {
            Console.WriteLine();
            logger.Error(text);
            Console.WriteLine();
        }
    }
}
