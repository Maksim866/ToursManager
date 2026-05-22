using HotToursManager.Services;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using HotToursManager.Storage.MsSql;

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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var repo = new MySqlTourRepository();
            var service = new TourService(repo);
            Application.Run(new MainForm(service));
        }
    }
}
