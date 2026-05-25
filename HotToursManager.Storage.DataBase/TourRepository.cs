using Microsoft.EntityFrameworkCore;
using HotToursManager.Models;
using HotToursManager.Storage.Contracts;

namespace HotToursManager.Storage.DataBase
{
    /// <summary>
    /// Репозиторий для работы с турами через MsSQL
    /// </summary>
    public class TourRepository : ITourRepository
    {
        private readonly TourDbContext dbContext;

        /// <summary>
        /// Конструктор с внедрением DbContext
        /// </summary>
        public TourRepository(TourDbContext сontext)
        {
            dbContext = сontext;
        }
        /// <summary>
        /// Получать все туры
        /// </summary>
        public async Task<List<Tour>> GetAllAsync()
        {
            var result = await dbContext.Tours
                .AsNoTracking()
                .OrderBy(t => t.Destination)
                .ToListAsync();
            return result;
        }

        /// <summary>
        /// Добавить тур
        /// </summary>
        public async Task AddAsync(Tour tour)
        {
            await dbContext.Tours.AddAsync(tour);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить тур
        /// </summary>
        public async Task UpdateAsync(Tour tour)
        {
            var existing = await dbContext.Tours.FindAsync(tour.Id);
            if (existing == null)
            {
                throw new InvalidOperationException($"Тур с ID {tour.Id} не найден. Невозможно обновить.");
            }

            existing.Destination = tour.Destination;
            existing.DepartureDate = tour.DepartureDate;
            existing.Nights = tour.Nights;
            existing.CostPerPerson = tour.CostPerPerson;
            existing.NumberOfPeople = tour.NumberOfPeople;
            existing.HasWiFi = tour.HasWiFi;
            existing.Surcharges = tour.Surcharges;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить тур по ID
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var tour = await dbContext.Tours.FindAsync(id);
            if (tour == null)
            {
                return;
            }

            dbContext.Tours.Remove(tour);
            await dbContext.SaveChangesAsync();
        }


        public async Task<Tour> GetByIdAsync(int id)
        {
            var result = await dbContext.Tours
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }
    }
}
