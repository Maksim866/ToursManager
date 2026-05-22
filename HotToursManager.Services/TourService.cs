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
        public Task<List<Tour>> GetAllToursAsync() => repo.GetAllAsync();

        /// <summary>
        /// Добавляет тур
        /// </summary>
        public Task AddTourAsync(Tour tour) => repo.AddAsync(tour);

        /// <summary>
        /// Обновляет тур
        /// </summary>
        public Task UpdateTourAsync(Tour tour) => repo.UpdateAsync(tour);

        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        public Task DeleteTourAsync(int id) => repo.DeleteAsync(id);

        /// <summary>
        /// Возвращает тур по ID
        /// </summary>
        public Task<Tour> GetTourByIdAsync(int id) => repo.GetByIdAsync(id);

        /// <summary>
        /// Возвращает статистику по турам
        /// </summary>
        public async Task<Statistics> GetStatisticsAsync()
        {
            var tours = await repo.GetAllAsync();
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
