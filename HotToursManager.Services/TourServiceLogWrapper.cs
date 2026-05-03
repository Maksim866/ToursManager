using HotToursManager.Models;
using HotToursManager.Services.Contracts;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HotToursManager.Services
{
    /// <summary>
    /// Враппер для логирования производительности методов TourService
    /// </summary>
    public class TourServiceLogWrapper : ITourService
    {
        private readonly ITourService mainService;
        private readonly ILogger<TourServiceLogWrapper> logger;

        /// <summary>
        /// Конструктор, принимающий сервис и логгер
        /// </summary>
        public TourServiceLogWrapper(ITourService mainService, ILogger<TourServiceLogWrapper> logger)
        {
            this.mainService = mainService;
            this.logger = logger;
        }

        /// <summary>
        /// Возвращает все туры с логированием производительности
        /// </summary>
        public List<Tour> GetAllTours()
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = mainService.GetAllTours();

            watch.Stop();
            var count = result != null ? result.Count : 0;
            logger.LogDebug(
                "TourService.GetAllTours: {ElapsedMilliseconds} ms. Count: {Count}",
                watch.ElapsedMilliseconds,
                count);

            return result;
        }
    }
}
