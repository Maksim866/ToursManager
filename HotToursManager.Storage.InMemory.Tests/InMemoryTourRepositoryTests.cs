using FluentAssertions;
using HotToursManager.Models;

namespace HotToursManager.Storage.InMemory.Tests
{

    /// <summary>
    /// Класс тестов для InMemoryTourRepository
    /// </summary>
    public class InMemoryTourRepositoryTests
    {
        private readonly InMemoryTourRepository repo;

        /// <summary>
        /// Создаёт новый репозиторий с начальными данными
        /// </summary>
        public InMemoryTourRepositoryTests()
        {
            repo = new InMemoryTourRepository();
        }

        /// <summary>
        /// Проверяет, что конструктор загружает 5 начальных туров
        /// </summary>
        [Fact]
        public async Task Ctor_SeedInitialData_5ToursLoaded()
        {
            //Arrange - уже сделано в конструкторе
            //Act
            var tours = await repo.GetAllAsync();
            //Assert
            tours.Should().HaveCount(5);
        }

        /// <summary>
        /// Проверяет, что GetById возвращает тур, если ID существует
        /// </summary>
        [Fact]
        public async Task GetById_ExistingId_ReturnsTour()
        {
            //Arrange
            const int id = 1;
            //Act
            var tour = await repo.GetByIdAsync(id);
            //Assert
            tour.Should().NotBeNull();
            tour.Id.Should().Be(id);
            tour.Destination.Should().Be("Турция");
        }

        /// <summary>
        /// Проверяет, что GetById возвращает null, если ID не существует
        /// </summary>
        [Fact]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            //Arrange
            const int id = 999;
            //Act
            var tour = await repo.GetByIdAsync(id);
            //Assert
            tour.Should().BeNull();
        }

        /// <summary>
        /// Проверяет, что GetAll возвращает копию списка, а не саму коллекцию
        /// </summary>
        [Fact]
        public async Task GetAll_ReturnsNewList_NotReference()
        {
            //Arrange - нет предусловий
            //Act
            var list1 = await repo.GetAllAsync();
            var list2 = await repo.GetAllAsync();
            //Assert
            list1.Should().NotBeSameAs(list2);
            list1.Should().HaveCount(5);
        }

        /// <summary>
        /// Проверяет, что Add присваивает следующий ID и добавляет в коллекцию
        /// </summary>
        [Fact]
        public async Task Add_NewTour_AssignNextIdAndAddsToCollection()
        {
            //Arrange
            var newTour = new Tour
            {
                Destination = "Мальдивы",
                Nights = 10,
                CostPerPerson = 120000,
                DepartureDate = new DateTime(2025, 1, 1),
                NumberOfPeople = 2,
                HasWiFi = true,
                Surcharges = 5000
            };
            //Act
            await repo.AddAsync(newTour);
            //Assert
            newTour.Id.Should().Be(6);
            (await repo.GetAllAsync()).Should().HaveCount(6);
            (await repo.GetByIdAsync(6)).Should().BeEquivalentTo(newTour, opt => opt.Excluding(t => t.Id));
        }

        /// <summary>
        /// Проверяет, что Add игнорирует ID, если он был задан заранее
        /// </summary>
        [Fact]
        public async Task Add_TourWithid_IgnoresIt_AndAssignsNewId()
        {
            //Arrange
            var tour = new Tour
            {
                Id = 999,
                Destination = "Берлин",
                Nights = 1,
                CostPerPerson = 10000,
                DepartureDate = new DateTime(2025, 1, 1),
                NumberOfPeople = 1,
                HasWiFi = false,
                Surcharges = 0
            };
            //Act
            await repo.AddAsync(tour);
            //Assert
            tour.Id.Should().Be(6);
            (await repo.GetByIdAsync(6)).Destination.Should().Be("Берлин");
        }

        ///<summary>
        ///Проверяет, что Update обновляет существующий тур
        ///</summary>
        [Fact]
        public async Task Update_ExistingTour_UpdatesCorrectly()
        {
            //Arrange
            var updated = new Tour
            {
                Id = 2,
                Destination = "Испания",
                Nights = 12,
                CostPerPerson = 70000,
                NumberOfPeople = 3,
                HasWiFi = false,
                Surcharges = 2300
            };
            //Act
            await repo.UpdateAsync(updated);
            //Assert
            var actual = await repo.GetByIdAsync(2);
            actual.Should().NotBeNull();
            actual.Nights.Should().Be(12);
            actual.Destination.Should().Be("Испания");
            actual.TotalCost.Should().Be(((70000 * 3) + 2300) * 12);
        }

        /// <summary>
        /// Проверяет, что Update не делает ничего, если ID не существует
        /// </summary>
        [Fact]
        public async Task Update_NonExistingId_DoesNothing()
        {
            //Arrange
            var tour = new Tour
            {
                Id = 999,
                Destination = "Париж",
                Nights = 1,
                CostPerPerson = 10000,
                DepartureDate = new DateTime(2025, 1, 1),
                NumberOfPeople = 1,
                HasWiFi = false,
                Surcharges = 0
            };
            //Act
            await repo.UpdateAsync(tour);
            //Assert
            (await repo.GetByIdAsync(999)).Should().BeNull();
            (await repo.GetAllAsync()).Should().HaveCount(5);
        }

        /// <summary>
        /// Проверяет, что Delete удаляет тур по существующему ID
        /// </summary>
        [Fact]
        public async Task Delete_ExisitngId_RemovesTour()
        {
            //Arrange
            const int id = 3;
            //Act
            await repo.DeleteAsync(id);
            //Assert
            (await repo.GetByIdAsync(id)).Should().BeNull();
            (await repo.GetAllAsync()).Should().HaveCount(4);
        }

        /// <summary>
        /// Проверяет, что Delete не делает ничего, если ID не существует
        /// </summary>
        [Fact]
        public async Task Delete_NonExistingId_DoesNothing()
        {
            //Arrange
            const int id = 999;
            //Act
            await repo.DeleteAsync(id);
            //Assert
            (await repo.GetAllAsync()).Should().HaveCount(5);
        }

        /// <summary>
        /// Проверяет, что удаление всех туров оставляет репозиторий пустым
        /// </summary>
        [Fact]
        public async Task Delete_AllTours_LeavesEmptyCollection()
        {
            //Arrange - 5 туров
            //Act
            foreach (var t in (await repo.GetAllAsync()).ToList())
            {
                await repo.DeleteAsync(t.Id);
            }
            //Assert
            (await repo.GetAllAsync()).Should().BeEmpty();
        }
    }
}
