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
    }
}
