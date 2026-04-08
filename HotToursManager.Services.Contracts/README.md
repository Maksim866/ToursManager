HotToursManager.Services.Contracts
📦 Библиотека контрактов сервисов для приложения HotToursManager.
📖 Описание
Данный пакет содержит основные интерфейсы и контракты бизнес-логики, используемые в приложении для управления турами.
Включает:

    интерфейсы сервисов (например, ITourService)
    DTO и прочие контракты для передачи данных между слоями

Пакет предназначен для повторного использования в различных слоях приложения:

    бизнес-логика (Business Logic)
    пользовательский интерфейс (WinForms)
    другие возможные клиенты

🚀 Установка
Через NuGet:

bash
1

или через Visual Studio:

    Manage NuGet Packages
    Найти HotToursManager.Services.Contracts
    Установить

🧩 Пример использования

using HotToursManager.Services.Contracts;
using HotToursManager.Models;

public class TourManager
{
    private readonly ITourService _tourService;

    public TourManager(ITourService tourService)
    {
        _tourService = tourService;
    }

    public void ProcessTour(int id)
    {
        var tour = _tourService.GetTourById(id);
        // Обработка...
    }
}

📌 Назначение
Пакет реализует слой контрактов и не содержит:

    реализации бизнес-логики
    пользовательского интерфейса

Это позволяет:

    использовать Dependency Injection
    переиспользовать контракты в разных проектах
    уменьшить связанность между слоями
    упростить тестирование и сопровождение системы

👤 Автор
Ваше имя
📄 Лицензия
Используется в учебных целях.
