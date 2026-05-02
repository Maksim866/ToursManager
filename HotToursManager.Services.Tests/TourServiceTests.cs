using FluentAssertions;
using HotToursManager.Models;
using Moq;
using HotToursManager.Storage.Contracts;
using HotToursManager.Services.Contracts;

namespace HotToursManager.Services.Tests
{
    /// <summary>
    /// Класс тестов для TourService
    /// </summary>
    public class TourServiceTests
    {
        private readonly Mock<ITourRepository> mockRepo;
        private readonly ITourService service;

        /// <summary>
        /// Создаёт экземпляр сервиса с мокированным репозиторием
        /// </summary>
        public TourServiceTests()
        {
            mockRepo = new Mock<ITourRepository>();
            service = new TourService(mockRepo.Object);
        }

        /// <summary>
        /// Проверяет, что GetAllTours возвращает список всех туров из репозитория
        /// </summary>
        [Fact]
        public void GetAllTours_ReturnsAllTours()
        {
            //Arrange
            var expectedTours = new List<Tour>
            {
                new Tour
                {
                    Id = 1,
                    Destination = "Турция",
                    DepartureDate = new DateTime(2024, 6, 15),
                    Nights = 7,
                    CostPerPerson = 45000,
                    NumberOfPeople = 2,
                    HasWiFi = true,
                    Surcharges = 1500
                },

                new Tour
                {
                    Id = 2,
                    Destination = "Испания",
                    DepartureDate = new DateTime(2024, 7, 20),
                    Nights = 10,
                    CostPerPerson = 62000,
                    NumberOfPeople = 3,
                    HasWiFi = false,
                    Surcharges = 2300
                }
            };
            mockRepo.Setup(r => r.GetAll()).Returns(expectedTours);
            //Act
            var result = service.GetAllTours();
            //Assert
            result.Should().BeEquivalentTo(expectedTours);
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        /// <summary>
        /// Проверяет, что GetAllTours возвращает пустой список, если репозиторий пуст
        /// </summary>
        [Fact]
        public void GetAllTours_EmptyRepository_ReturnsEmptyList()
        {
            //Arrange
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Tour>());
            //Act
            var result = service.GetAllTours();
            //Assert
            result.Should().BeEmpty();
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        /// <summary>
        /// Проверяет, что AddTour передаёт тур в репозиторий для сохранения
        /// </summary>
        [Fact]
        public void AddTour_DelegatesToRepository()
        {
            //Arrange
            var tour = new Tour
            {
                Id = 0,
                Destination = "Бали",
                DepartureDate = new DateTime(2025, 3, 10),
                Nights = 5,
                CostPerPerson = 80000,
                NumberOfPeople = 2,
                HasWiFi = true,
                Surcharges = 0
            };
            //Act
            service.AddTour(tour);
            //Assert
            mockRepo.Verify(r => r.Add(tour), Times.Once);
        }

        /// <summary>
        ///Проверяет, что UpdateTour передает обновленный тур в репозиторий
        /// </summary>
        [Fact]
        public void UpdateTour_DelegatesToRepository()
        {
            //Arrange
            var tour = new Tour
            {
                Id = 5,
                Destination = "Франция",
                DepartureDate = new DateTime(2024, 9, 10),
                Nights = 8,
                CostPerPerson = 35000m,
                NumberOfPeople = 4,
                HasWiFi = true,
                Surcharges = 800m
            };
            //Act
            service.UpdateTour(tour);
            //Assert
            mockRepo.Verify(r => r.Update(tour), Times.Once);
        }

        /// <summary>
        /// Проверяет, что DeleteTour вызывает удаление в репозитории по ID
        /// </summary>
        [Fact]
        public void DeleteTour_DelegatesToRepository()
        {
            //Arrange
            const int tourId = 3;
            //Act
            service.DeleteTour(tourId);
            //Assert
            mockRepo.Verify(r => r.Delete(tourId), Times.Once);
        }

        /// <summary>
        /// Проверяет, что GetTourById возвращает тур при наличии в репозитории
        /// </summary>
        [Fact]
        public void GetTourById_Existing_ReturnsTour()
        {
            //Arrange
            const int id = 1;
            var expectedTour = new Tour
            {
                Id = id,
                Destination = "Турция",
                DepartureDate = new DateTime(2024, 6, 15),
                Nights = 7,
                CostPerPerson = 45000,
                NumberOfPeople = 2,
                HasWiFi = true,
                Surcharges = 1500
            };
            mockRepo.Setup(r => r.GetById(id)).Returns(expectedTour);
            //Act
            var result = service.GetTourById(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedTour);
            mockRepo.Verify(r => r.GetById(id), Times.Once);
        }

        /// <summary>
        /// Проверяет, что GetTourById возвращает null при отсутствии тура в репозитории
        /// </summary>
        [Fact]
        public void GetTourById_NonExisting_ReturnsNull()
        {
            //Arrange
            const int id = 999;
            mockRepo.Setup(r => r.GetById(id)).Returns((Tour)null);
            //Act
            var result = service.GetTourById(id);
            //Assert
            result.Should().BeNull();
            mockRepo.Verify(r => r.GetById(id), Times.Once);
        }

        /// <summary>
        /// Проверяет корректность расчёта статистики по списку туров
        /// </summary>
        [Fact]
        public void GetStatistics_CalculatesCorrectly()
        {
            //Arrange
            var tours = new List<Tour>
            {
                new Tour
                {
                    Id = 1,
                    Destination = "Турция",
                    DepartureDate = new DateTime(2024, 6, 15),
                    Nights = 5,
                    CostPerPerson = 10000m,
                    NumberOfPeople = 2,
                    HasWiFi = true,
                    Surcharges = 500m
                },
                new Tour
                {
                    Id = 2,
                    Destination = "Испания",
                    DepartureDate = new DateTime(2024, 7, 20),
                    Nights = 10,
                    CostPerPerson = 20000m,
                    NumberOfPeople = 1,
                    HasWiFi = false,
                    Surcharges = 0m
                },
                new Tour
                {
                    Id = 3,
                    Destination = "Италия",
                    DepartureDate = new DateTime(2024, 8, 5),
                    Nights = 2,
                    CostPerPerson = 5000m,
                    NumberOfPeople = 3,
                    HasWiFi = true,
                    Surcharges = 1000m
                }
            };
            mockRepo.Setup(r => r.GetAll()).Returns(tours);

            var tour1Cost = ((10000m * 2) + 500m) * 5;
            var tour2Cost = ((20000m * 1) + 0m) * 10;
            var tour3Cost = ((5000m * 3) + 1000m) * 2;
            var expectedTotalCost = tour1Cost + tour2Cost + tour3Cost;

            var expectedTotalTours = 3;
            var expectedToursWithSurcharges = 2;
            var expectedTotalSurcharges = 500m + 0m + 1000m;
            //Act
            var stats = service.GetStatistics();
            //Assert
            stats.TotalTours.Should().Be(expectedTotalTours);
            stats.TotalCost.Should().Be(expectedTotalCost);
            stats.ToursWithSurcharges.Should().Be(expectedToursWithSurcharges);
            stats.TotalSurcharges.Should().Be(expectedTotalSurcharges);
            mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        /// <summary>
        /// Проверяет, что статистика возвращает нули при пустом репозитории
        /// </summary>
        [Fact]
        public void GetStatistics_EmptyRepository_ReturnsZero()
        {
            //Arrange
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Tour>());
            //Act
            var stats = service.GetStatistics();
            //Assert
            stats.TotalTours.Should().Be(0);
            stats.TotalCost.Should().Be(0);
            stats.ToursWithSurcharges.Should().Be(0);
            stats.TotalSurcharges.Should().Be(0);
            mockRepo.Verify(r => r.GetAll(), Times.Once());
        }
    }
}
