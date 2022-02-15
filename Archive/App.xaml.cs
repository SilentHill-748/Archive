using Archive.Logic.Services;
using Archive.Logic.Services.Interfaces;

using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Archive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string LOG_FILE = "C:\\Logs\\log.txt";
        private readonly static IDbService _dbService;


        static App()
        {
            _dbService = ServiceFactory.GetService<IDbService>();
        }


        public static IDbService DbService => _dbService;


        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            CreateLogFileIfNotExists();

            string oldLogs = ReadOldLogs();

            string message = e.Exception.Message;
            string stackTrace = e.Exception.StackTrace ?? "No StackTrace.";

            string log = $"{DateTime.UtcNow.AddHours(5)}:\n{message}\nStackTrace:\n{stackTrace}\n\n{oldLogs}";

            WriteLog(log);

            e.Handled = true;
        }

        private static string ReadOldLogs()
        {
            using StreamReader reader = new(LOG_FILE);
            return reader.ReadToEnd();
        }

        private static void WriteLog(string message)
        {
            using StreamWriter streamWriter = new(LOG_FILE, false);
            streamWriter.WriteLine(message);
        }

        private void CreateLogFileIfNotExists()
        {
            if (!File.Exists(LOG_FILE))
            {
                if (!Directory.Exists(LOG_FILE))
                    Directory.CreateDirectory(LOG_FILE[..LOG_FILE.LastIndexOf("\\")]);

                File.Create(LOG_FILE).Close();
            }
        }
    }
}
