using Microsoft.EntityFrameworkCore;
using HotToursManager.Models;
using HotToursManager.Storage.Contracts;

namespace HotToursManager.Storage.MsSql
{
    /// <summary>
    /// Репозиторий для работы с турами через MsSQL
    /// </summary>
    public class MySqlTourRepository : ITourRepository
    {
        /// <summary>
        /// Получать все туры
        /// </summary>
        public async Task<List<Tour>> GetAllAsync()
        {
            using var db = new TourDbContext();
            var result = await db.Tours
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
            using var db = new TourDbContext();
            await db.Tours.AddAsync(tour);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить тур
        /// </summary>
        public async Task UpdateAsync(Tour tour)
        {
            using var db = new TourDbContext();
            var existing = await db.Tours.FindAsync(tour.Id);
            if (existing == null)
            {
                return;
            }

            existing.Destination = tour.Destination;
            existing.DepartureDate = tour.DepartureDate;
            existing.Nights = tour.Nights;
            existing.CostPerPerson = tour.CostPerPerson;
            existing.NumberOfPeople = tour.NumberOfPeople;
            existing.HasWiFi = tour.HasWiFi;
            existing.Surcharges = tour.Surcharges;

            db.Tours.Update(existing);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить тур по ID
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            using var db = new TourDbContext();
            var tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return;
            }

            db.Tours.Remove(tour);
            await db.SaveChangesAsync();
        }


        public async Task<Tour> GetByIdAsync(int id)
        {
            using var db = new TourDbContext();
            var result = await db.Tours
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }
    }
}
