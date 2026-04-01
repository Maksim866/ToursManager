namespace HotToursManager.Desktop.Constants
{
    /// <summary>
    /// Бизнес-ограничения для туров.
    /// </summary>
    public static class TourLimits
    {
        /// <summary>
        /// Минимальное количество ночей в туре.
        /// </summary>
        public const int MinNights = 1;

        /// <summary>
        /// Максимальное количество ночей в туре.
        /// </summary>
        public const int MaxNights = 99;

        /// <summary>
        /// Минимальное количество отдыхающих в туре.
        /// </summary>
        public const int MinPeople = 1;

        /// <summary>
        /// Максимальное количество отдыхающих в туре.
        /// </summary>
        public const int MaxPeople = 5;

        /// <summary>
        /// Минимальная стоимость за одного отдыхающего (в рублях).
        /// </summary>
        public const decimal MinCostPerPerson = 1000m;

        /// <summary>
        /// Максимальная стоимость за одного отдыхающего (в рублях).
        /// </summary>
        public const decimal MaxCostPerPerson = 500_000m;

        /// <summary>
        /// Минимальная сумма доплат (в рублях).
        /// </summary>
        public const decimal MinSurcharge = 0m;

        /// <summary>
        /// Максимальная сумма доплат (в рублях).
        /// </summary>
        public const decimal MaxSurcharge = 100_000m;

        /// <summary>
        /// Проверяет, что количество ночей в допустимом диапазоне.
        /// </summary>
        public static bool IsValidNights(int nights) =>
            nights >= MinNights && nights <= MaxNights;

        /// <summary>
        /// Проверяет, что количество отдыхающих в допустимом диапазоне.
        /// </summary>
        public static bool IsValidPeople(int people) =>
            people >= MinPeople && people <= MaxPeople;

        /// <summary>
        /// Проверяет, что стоимость за одного отдыхающего в допустимом диапазоне.
        /// </summary>
        public static bool IsValidCostPerPerson(decimal cost) =>
            cost >= MinCostPerPerson && cost <= MaxCostPerPerson;

        /// <summary>
        /// Проверяет, что сумма доплат в допустимом диапазоне.
        /// </summary>
        public static bool IsValidSurcharge(decimal surcharge) =>
            surcharge >= MinSurcharge && surcharge <= MaxSurcharge;

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимого количества ночей.
        /// </summary>
        public static string GetNightsError() =>
            $"Количество ночей должно быть от {MinNights} до {MaxNights}.";

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимого количества отдыхающих
        /// </summary>
        public static string GetPeopleError() =>
            $"Количество отдыхающих должно быть от {MinPeople} до {MaxPeople}.";

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимой стоимости за человека.
        /// </summary>
        public static string GetCostError() =>
            $"Стоимость за чел. должна быть от {MinCostPerPerson:N0} до {MaxCostPerPerson:N0} руб.";

        /// <summary>
        /// Возвращает сообщение об ошибке для недопустимой суммы доплат.
        /// </summary>
        public static string GetSurchargeError() =>
            $"Доплата должна быть от {MinSurcharge:N0} до {MaxSurcharge:N0} руб.";
    }
}
