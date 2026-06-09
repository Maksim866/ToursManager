using HotToursManager.Models;
using Microsoft.EntityFrameworkCore;
using HotToursManager.Storage.Contracts;

namespace HotToursManager.Storage.DataBase
{
    /// <summary>
    /// Контекст базы данных для управления турами
    /// </summary>
    public class TourDbContext : DbContext, IReader, IWriter
    {
        /// <summary>
        /// Таблица туров
        /// </summary>
        public DbSet<Tour> Tours { get; set; }

        /// <summary>
        /// Конструктор с автоматическим созданием БД
        /// </summary>
        public TourDbContext() => Database.EnsureCreated();

        public TourDbContext(DbContextOptions<TourDbContext> options)
           : base(options)
        {
        }

        /// <summary>
        /// Настройка подключения к БД
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HotToursManager;Username=postgres;Password=14082002");
        }

        /// <summary>
        /// Получение данных из БД без отслеживания изменений
        /// </summary>
        IQueryable<TEntity> IReader.Reader<TEntity>()
        {
            return base.Set<TEntity>()
                       .AsNoTracking()
                       .AsQueryable();
        }

        void IWriter.Add<TEntity>(TEntity entity)
        {
            base.Add(entity);
        }

        void IWriter.Update<TEntity>(TEntity entity)
        {
            base.Update(entity);
        }

        void IWriter.Delete<TEntity>(TEntity entity)
        {
            base.Remove(entity);
        }
    }
}
