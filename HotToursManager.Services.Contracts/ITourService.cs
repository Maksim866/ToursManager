using HotToursManager.Models;

namespace HotToursManager.Services.Contracts
{
    public interface ITourService
    {
        /// <summary>
        /// Возвращает все туры
        /// </summary>
        Task<List<Tour>> GetAllToursAsync();

        /// <summary>
        /// Добавляет новый тур
        /// </summary>
        Task AddTourAsync(Tour tour);

        /// <summary>
        /// Обновляет тур
        /// </summary>
        Task UpdateTourAsync(Tour tour);

        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        Task DeleteTourAsync(int id);

        /// <summary>
        /// Возвращает тур по ID
        /// </summary>
        Task<Tour> GetTourByIdAsync(int id);

        /// <summary>
        /// Возвращает статистику по турам
        /// </summary>
        Task<Statistics> GetStatisticsAsync();
    }
}
