# Задача:DataGridView (DGV)

ToursManager

Калинкин Максим Леонидович

https://t.me/Jarvis570

ИП-24-4

# upd:06.04.2026

Задание: Создание собственного nuget-пакета

Вынес проект HotToursManager.Model, HotToursManager.Services.Contracts, HotToursManager.Storage.Constracts в Nuget-библиотеку и подключил через пакет Nuget

# upd:25.04.2026

Задание: Написание unit тестов

Создал unit-тесты для HotToursManager.Services и HotToursManager.Storage.InMemory с использованием xUnit, FluentAssertions и Moq, добившись 100% покрытия кода.

# upd:04.05.2026

Задание: Логирование производительности и сбор логов в web интерфейс

Создал враппер TourServiceLogWrapper для логирования производительности всех методов TourService с замером времени через Stopwatch. Настроил Serilog с отправкой логов в Seq (с API Key), файлы и Debug. Все логи структурированы и доступны для анализа в web-интерфейсе Seq.
