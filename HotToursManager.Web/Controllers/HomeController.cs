using System.Diagnostics;
using HotToursManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotToursManager.Web.Controllers
{

    /// <summary>
    /// Контроллер главной страницы приложения
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница приложения
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Страница конфиденциальности
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Страница отображения ошибок приложения
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
