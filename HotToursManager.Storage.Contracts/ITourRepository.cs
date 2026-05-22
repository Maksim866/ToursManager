using HotToursManager.Models;

namespace HotToursManager.Storage.Contracts
{
    /// <summary>
    /// Интерфейс для работы с данными туров в хранилище
    /// </summary>
    public interface ITourRepository
    {
        /// <summary>
        /// Получает список всех туров
        /// </summary>
        Task<List<Tour>> GetAllAsync();
        /// <summary>
        /// Получает тур по ID
        /// </summary>
        Task<Tour> GetByIdAsync(int id);
        /// <summary>
        /// Обновляет информацию о туре
        /// </summary>
        Task UpdateAsync(Tour tour);
        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        Task DeleteAsync(int id);
        /// <summary>
        /// Добавляет новый тур
        /// </summary>
        Task AddAsync(Tour tour);
    }
}
