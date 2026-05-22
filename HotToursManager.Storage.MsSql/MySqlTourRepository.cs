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
        public List<Tour> GetAll()
        {
            using var db = new TourDbContext();
            var result = db.Tours
                .AsNoTracking()
                .OrderBy(t => t.Destination)
                .ToList();
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
        public void Update(Tour tour)
        {
            using var db = new TourDbContext();
            var existing = db.Tours.Find(tour.Id);
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
            db.SaveChanges();
        }

        /// <summary>
        /// Удалить тур по ID
        /// </summary>
        public void Delete(int id)
        {
            using var db = new TourDbContext();
            var tour = db.Tours.Find(id);
            if (tour == null)
            {
                return;
            }

            db.Tours.Remove(tour);
            db.SaveChanges();
        }


        public Tour GetById(int id)
        {
            using var db = new TourDbContext();
            var result = db.Tours
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
            return result;
        }
    }
}
