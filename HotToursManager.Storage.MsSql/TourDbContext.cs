using HotToursManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HotToursManager.Storage.MsSql
{
    /// <summary>
    /// Контекст базы данных для управления турами
    /// </summary>
    public class TourDbContext : DbContext
    {
        /// <summary>
        /// Таблица туров
        /// </summary>
        public DbSet<Tour> Tours { get; set; }

        /// <summary>
        /// Конструктор с автоматическим созданием БД
        /// </summary>
        public TourDbContext() => Database.EnsureCreated();

        /// <summary>
        /// Настройка подключения к БД
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HotToursManager;Username=postgres;Password=14082002");
        }
    }
}
