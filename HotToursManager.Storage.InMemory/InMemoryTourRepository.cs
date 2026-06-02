using HotToursManager.Models;
using HotToursManager.Storage.Contracts;

namespace HotToursManager.Storage.InMemory
{
    /// <summary>
    /// Реализация репозитория туров, хранящего данные в памяти
    /// </summary>
    public class InMemoryTourRepository : ITourRepository
    {
        private readonly List<Tour> tours;
        private int nextId = 1;
        /// <summary>
        /// Инициализирует репозиторий начальными данными
        /// </summary>
        public InMemoryTourRepository()
        {
            tours = SeedInitialData();
        }
        private static List<Tour> SeedInitialData()
        {
            return
            [
                new() {
                    Destination = "Турция",
                    DepartureDate = new DateTime(2024, 6, 15),
                    Nights = 7,
                    CostPerPerson = 45000,
                    NumberOfPeople = 2,
                    HasWiFi = true,
                    Surcharges = 1500,
                },
                new() {
                    Destination = "Испания",
                    DepartureDate = new DateTime(2024, 7, 20),
                    Nights = 10,
                    CostPerPerson = 62000,
                    NumberOfPeople = 3,
                    HasWiFi = false,
                    Surcharges = 2300,
                },
                new() {
                    Destination = "Италия",
                    DepartureDate = new DateTime(2024, 8, 5),
                    Nights = 5,
                    CostPerPerson = 28000,
                    NumberOfPeople = 1,
                    HasWiFi = true,
                    Surcharges = 0
                },
                new() {
                    Destination = "Франция",
                    DepartureDate = new DateTime(2024, 9, 10),
                    Nights = 8,
                    CostPerPerson = 35000,
                    NumberOfPeople = 4,
                    HasWiFi = true,
                    Surcharges = 800
                },
                new() {
                    Destination = "Шушары",
                    DepartureDate = new DateTime(2024, 10, 1),
                    Nights = 14,
                    CostPerPerson = 89000,
                    NumberOfPeople = 2,
                    HasWiFi = true,
                    Surcharges = 3450
                }
            ];
        }
        /// <summary>
        /// Возвращает все туры из памяти
        /// </summary>
        public Task<List<Tour>> GetAllAsync() => Task.FromResult(new List<Tour>(tours));
        /// <summary>
        /// Возвращает тур по ID
        /// </summary>
        public Task<Tour> GetByIdAsync(int id) => Task.FromResult(tours.FirstOrDefault(t => t.Id == id));
        /// <summary>
        /// Добавляет тур в память
        /// </summary>
        public Task AddAsync(Tour tour)
        {
            tour.Id = nextId++;
            tours.Add(tour);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Обновляет тур по ID
        /// </summary>
        public Task UpdateAsync(Tour tour)
        {
            var index = tours.FindIndex(t => t.Id == tour.Id);
            if (index >= 0)
            {
                tours[index] = tour;
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        public Task DeleteAsync(int id)
        {
            tours.RemoveAll(t => t.Id == id);
            return Task.CompletedTask;
        }
    }
}
