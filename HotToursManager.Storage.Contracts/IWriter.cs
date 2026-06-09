using System.Diagnostics.CodeAnalysis;

namespace HotToursManager.Storage.Contracts
{
    /// <summary>
    /// Интерфейс создания, обновления и удаления записей в хранилище
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Добавляет новую запись в хранилище
        /// </summary>
        void Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

        /// <summary>
        /// Обновляет существующую запись в хранилище
        /// </summary>
        void Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

        /// <summary>
        /// Удаляет существующую запись из хранилища
        /// </summary>
        void Delete<TEntity>([NotNull] TEntity entity) where TEntity : class;

        /// <summary>
        /// Асинхронно сохраняет изменения в хранилище
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
