using HotToursManager.Services;
using HotToursManager.Storage.InMemory;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace HotToursManager.Desktop.Forms
{
    /// <summary>
    /// Главный класс программы, содержащий точку входа в приложение.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.File("logs/tour-perf-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var loggerFactory = new SerilogLoggerFactory(serilogLogger);
            var logger = loggerFactory.CreateLogger<TourServiceLogWrapper>();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var repo = new InMemoryTourRepository();
            var service = new TourService(repo);
            var loggingWrapper = new TourServiceLogWrapper(service, logger);

            Application.Run(new MainForm(loggingWrapper));

            Log.CloseAndFlush();
        }
    }
}
