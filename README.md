Калинкин Максим Леонидович  https://t.me/Jarvis570

ИП-24-4

Задание: DataGridView

Вариант: 4 - горящие туры

# upd:23.05.2026

Задание: Работа с базой данных через ORM EF Core

Добавил EF, создал контекст БД и репозиторий Туров. Перевел сервисы и репозитории на асинхронную работу.

# upd:04.05.2026

Задание: Логирование производительности и сбор логов в web интерфейс

Создал враппер TourServiceLogWrapper для логирования производительности всех методов TourService с замером времени через Stopwatch. Настроил Serilog с отправкой логов в Seq (с API Key), файлы и Debug. Все логи структурированы и доступны для анализа в web-интерфейсе Seq.

# upd:25.04.2026
Задание: Написание unit тестов

Написал юнит тесты к HotToursManager.Services и HotToursManfger.Storage.InMemory, с использованием FluentAssertions и Moq

# upd:06.04.2026

Задание: Создание собственного nuget-пакета

Вынес проект HotToursManager.Model, HotToursManager.Services.Contracts, HotToursManager.Storage.Constracts в Nuget-библиотеку и подключил через пакет Nuget
