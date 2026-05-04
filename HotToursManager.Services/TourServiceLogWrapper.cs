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

        /// <summary>
        /// Добавляет тур с логированием производительности
        /// </summary>
        public void AddTour(Tour tour)
        {
            var watch = Stopwatch.StartNew();
            watch.Start();

            mainService.AddTour(tour);

            watch.Stop();
            var destination = tour != null ? tour.Destination : null;
            logger.LogDebug(
                   "TourService.AddTour: {ElapsedMilliseconds} ms. Destination: {Destination}",
                    watch.ElapsedMilliseconds,
                    destination);
        }

        /// <summary>
        /// Обновляет тур с логированием производительности
        /// </summary>
        public void UpdateTour(Tour tour)
        {
            var watch = new Stopwatch();
            watch.Start();

            mainService.UpdateTour(tour);

            watch.Stop();
            var destination = tour != null ? tour.Destination : null;
            var id = tour != null ? tour.Id : 0;
            logger.LogDebug(
                   "TourService.UpdateTour: {ElapsedMilliseconds} ms. Id: {Id}, Destination: {Destination}",
                     watch.ElapsedMilliseconds,
                     id,
                     destination);
        }

        /// <summary>
        /// Удаляет тур по ID с логированием производительности
        /// </summary>
        public void DeleteTour(int id)
        {
            var watch = new Stopwatch();
            watch.Start();

            mainService.DeleteTour(id);

            watch.Stop();
            logger.LogDebug(
                    "TourService.DeleteTour: {ElapsedMilliseconds} ms. id: {id}",
                     watch.ElapsedMilliseconds,
                     id);
        }

        /// <summary>
        /// Возвращает тур по ID с логированием производительности
        /// </summary>
        public Tour GetTourById(int id)
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = mainService.GetTourById(id);

            watch.Stop();
            var found = result != null;
            logger.LogDebug(
                    "TourService.GetTourById: {ElapsedMilliseconds} ms. Id: {Id}, Found: {Found}",
                      watch.ElapsedMilliseconds,
                      found,
                      id);
            return result;
        }

        /// <summary>
        /// Возвращает статистику по турам с логированием производительности
        /// </summary>
        public Statistics GetStatistics()
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = mainService.GetStatistics();

            watch.Stop();
            var totalCost = result != null ? result.TotalCost : 0;
            var totalTours = result != null ? result.TotalTours : 0;
            var totalWithSurcharges = result != null ? result.ToursWithSurcharges : 0;
            var totalSurcharges = result != null ? result.TotalSurcharges : 0;

            logger.LogDebug(
                    "TourServuce.GetStatistics: {ElapsedMilliseconds} ms. TotalTours: {TotalTours}, TotalCost: {TotalCost}," +
                    " ToursWithSurcharges: {ToursWithSurcharges}, TotalSurcharges: {TotalSurcharges}",
                    watch.ElapsedMilliseconds,
                    totalTours,
                    totalCost,
                    totalWithSurcharges,
                    totalSurcharges);
            return result;
        }
    }
}
