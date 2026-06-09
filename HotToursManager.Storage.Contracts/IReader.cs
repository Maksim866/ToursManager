
namespace HotToursManager.Storage.Contracts
{
    /// <summary>
    /// Интерфейс получения записей из хранилища
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary>
        IQueryable<TEntity> Read<TEntity>() where TEntity : class;
    }
}
