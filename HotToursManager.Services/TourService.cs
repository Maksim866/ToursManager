using HotToursManager.Models;
using HotToursManager.Services.Contracts;
using HotToursManager.Storage.Contracts;

namespace HotToursManager.Services
{
    /// <summary>
    /// Реализация бизнес-логики для работы с турами
    /// </summary>
    public class TourService : ITourService
    {
        private readonly ITourRepository repo;

        /// <summary>
        /// Создаёт сервис с указанным репозиторием
        /// </summary>
        public TourService(ITourRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Возвращает все туры
        /// </summary>
        public List<Tour> GetAllTours() => repo.GetAll();

        /// <summary>
        /// Добавляет тур
        /// </summary>
        public void AddTour(Tour tour) => repo.Add(tour);

        /// <summary>
        /// Обновляет тур
        /// </summary>
        public void UpdateTour(Tour tour) => repo.Update(tour);

        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        public void DeleteTour(int id) => repo.Delete(id);

        /// <summary>
        /// Возвращает тур по ID
        /// </summary>
        public Tour GetTourById(int id) => repo.GetById(id);

        /// <summary>
        /// Возвращает статистику по турам
        /// </summary>
        public Statistics GetStatistics()
        {
            var tours = repo.GetAll();
            return new Statistics
            {
                TotalTours = tours.Count,
                TotalCost = tours.Sum(t => t.TotalCost),
                ToursWithSurcharges = tours.Count(t => t.Surcharges > 0),
                TotalSurcharges = tours.Sum(t => t.Surcharges)
            };
        }
    }
}
