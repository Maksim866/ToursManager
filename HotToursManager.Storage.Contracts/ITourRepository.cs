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
        List<Tour> GetAll();
        /// <summary>
        /// Получает тур по ID
        /// </summary>
        Tour GetById(int id);
        /// <summary>
        /// Обновляет информацию о туре
        /// </summary>
        void Update(Tour tour);
        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        void Delete(int id);
        /// <summary>
        /// Добавляет новый тур
        /// </summary>
        void Add(Tour tour);
    }
}
