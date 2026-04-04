using HotToursManager.Models;

namespace HotToursManager.Services.Contracts
{
    public interface ITourService
    {
        /// <summary>
        /// Возвращает все туры
        /// </summary>
        List<Tour> GetAllTours();

        /// <summary>
        /// Добавляет новый тур
        /// </summary>
        void AddTour(Tour tour);

        /// <summary>
        /// Обновляет тур
        /// </summary>
        void UpdateTour(Tour tour);

        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        void DeleteTour(int id);

        /// <summary>
        /// Возвращает тур по ID
        /// </summary>
        Tour GetTourById(int id);

        /// <summary>
        /// Возвращает статистику по турам
        /// </summary>
        Statistics GetStatistics();
    }
}
