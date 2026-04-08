HotToursManager.Storage.Contracts
📦 Библиотека контрактов хранилища данных для приложения HotToursManager.
📖 Описание
Данный пакет содержит основные интерфейсы и контракты для работы с хранилищем данных в приложении управления турами.
Включает:

    интерфейсы репозиториев (например, ITourRepository)
    базовые контракты для доступа к данным
    интерфейсы для работы с различными источниками данных

Пакет предназначен для повторного использования в различных слоях приложения:

    слой данных (Data Access Layer)
    бизнес-логика (Business Logic)
    тестирование (Unit Tests)

🚀 Установка
Через NuGet:

bash
1

или через Visual Studio:

    Manage NuGet Packages
    Найти HotToursManager.Storage.Contracts
    Установить

🧩 Пример использования

using HotToursManager.Storage.Contracts;
using HotToursManager.Models;

public class TourService
{
    private readonly ITourRepository _tourRepository;

    public TourService(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
    }

    public List<Tour> GetAllTours()
    {
        return _tourRepository.GetAll();
    }

    public Tour GetTourById(int id)
    {
        return _tourRepository.GetById(id);
    }
}

📌 Назначение
Пакет реализует слой контрактов хранения и не содержит:

    конкретных реализаций репозиториев
    подключений к базам данных
    пользовательского интерфейса

Это позволяет:

    использовать Dependency Injection
    переиспользовать контракты в разных проектах
    легко заменять реализации (In-Memory, SQL, NoSQL)
    уменьшить связанность между слоями
    упростить тестирование и сопровождение системы

👤 Автор
Ваше имя
📄 Лицензия
Используется в учебных целях.
