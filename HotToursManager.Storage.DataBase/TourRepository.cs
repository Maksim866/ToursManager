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
        private readonly IReader reader;
        private readonly IWriter writer;

        /// <summary>
        /// Конструктор с внедрением DbContext
        /// </summary>
        public TourRepository(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        /// <summary>
        /// Получать все туры
        /// </summary>
        public async Task<List<Tour>> GetAllAsync()
        {
            var result = await reader.Read<Tour>()
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
            writer.Add(tour);
            await writer.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить тур
        /// </summary>
        public async Task UpdateAsync(Tour tour)
        {
            var existing = await reader.Read<Tour>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == tour.Id);
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

            writer.Update(existing);
            await writer.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить тур по ID
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var tour = await reader.Read<Tour>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tour == null)
            {
                return;
            }

            writer.Delete(tour);
            await writer.SaveChangesAsync();
        }

        /// <summary>
        /// Получить тур по ID
        /// </summary>
        public async Task<Tour> GetByIdAsync(int id)
        {
            var result = await reader.Read<Tour>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }
    }
}
