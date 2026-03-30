using HotToursManager.Models;
using HotToursManager.Storage.Contracts;

namespace HotToursManager.Storage.InMemory
{
    /// <summary>
    /// Реализация репозитория туров, хранящего данные в памяти
    /// </summary>
    public class InMemoryTourRepository : ITourRepository
    {
        private List<Tour> tours = new();
        private int nextId = 1;
        /// <summary>
        /// Инициализирует репозиторий начальными данными
        /// </summary>
        public InMemoryTourRepository()
        {
            tours.Add(new Tour
            {
                Id = nextId++,
                Destination = "Турция",
                DepartureDate = new DateTime(2024, 6, 15),
                Nights = 7,
                CostPerPerson = 45000,
                NumberOfPeople = 2,
                HasWiFi = true,
                Surcharges = 1500,
            });

            tours.Add(new Tour
            {
                Id = nextId++,
                Destination = "Испания",
                DepartureDate = new DateTime(2024, 7, 20),
                Nights = 10,
                CostPerPerson = 62000,
                NumberOfPeople = 3,
                HasWiFi = false,
                Surcharges = 2300,
            });
            tours.Add(new Tour
            {
                Id = nextId++,
                Destination = "Италия",
                DepartureDate = new DateTime(2024, 8, 5),
                Nights = 5,
                CostPerPerson = 28000,
                NumberOfPeople = 1,
                HasWiFi = true,
                Surcharges = 0
            });
            tours.Add(new Tour
            {
                Id = nextId++,
                Destination = "Франция",
                DepartureDate = new DateTime(2024, 9, 10),
                Nights = 8,
                CostPerPerson = 35000,
                NumberOfPeople = 4,
                HasWiFi = true,
                Surcharges = 800
            });
            tours.Add(new Tour
            {
                Id = nextId++,
                Destination = "Шушары",
                DepartureDate = new DateTime(2024, 10, 1),
                Nights = 14,
                CostPerPerson = 89000,
                NumberOfPeople = 2,
                HasWiFi = true,
                Surcharges = 3450
            });
        }
        /// <summary>
        /// Возвращает все туры из памяти
        /// </summary>
        public List<Tour> GetAll() => new(tours);
        /// <summary>
        /// Возвращает тур по ID
        /// </summary>
        public Tour GetById(int id) => tours.FirstOrDefault(t => t.Id == id);
        /// <summary>
        /// Добавляет тур в память
        /// </summary>
        public void Add(Tour tour)
        {
            tour.Id = nextId++;
            tours.Add(tour);
        }
        /// <summary>
        /// Обновляет тур по ID
        /// </summary>
        public void Update(Tour tour)
        {
            var index = tours.FindIndex(t => t.Id == tour.Id);
            if (index >= 0)
            {
                tours[index] = tour;
            }
        }
        /// <summary>
        /// Удаляет тур по ID
        /// </summary>
        public void Delete(int id) => tours.RemoveAll(t => t.Id == id);
    }
}
