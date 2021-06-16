using System;
using System.IO;

namespace NickX.InventoryManagement.Utils.Logging
{
    public class Whistleblower
    {
        public string LogDirectory { get; set; }
        public LoggingLevels LogLevel { get; set; }

        public Whistleblower(String logDirectory, LoggingLevels logLevel = LoggingLevels.INFO)
        {
            this.LogDirectory = logDirectory;
            this.LogLevel = logLevel;
        }

        public void Log(String message, LoggingLevels logLevel)
        {

            if ((int)logLevel > (int)this.LogLevel)
                return;

            var now = DateTime.Now;
            var file = Path.Combine(LogDirectory, now.ToString("dd-MM-yyyy") + ".log");

            Directory.CreateDirectory(LogDirectory);
            if (!File.Exists(file))
                File.Create(file).Dispose();

            var finalMsg = $"[{now.ToString("hh:mm:ss")}] {logLevel} >> {message}" + Environment.NewLine;
            File.AppendAllText(file, finalMsg);
        }
    }

    public enum LoggingLevels
    {
        DEBUG = 0,
        ERROR = 1,
        WARNING = 2,
        INFO = 3
    }
}
