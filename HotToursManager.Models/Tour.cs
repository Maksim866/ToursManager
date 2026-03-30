namespace HotToursManager.Models
{
    /// <summary>
    /// Модель тура
    /// </summary>
    public class Tour
    {
        /// <summary>
        /// Уникальный индификатор тура
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Направление тура (например Испания, Турция)
        /// </summary>
        public string Destination { get; set; } = "";
        /// <summary>
        /// Дата вылета
        /// </summary>
        public DateTime DepartureDate { get; set; }
        /// <summary>
        /// Количество ночей
        /// </summary>
        public int Nights { get; set; }
        /// <summary>
        /// Стоимость за одного отдыхающего (в рублях)
        /// </summary>
        public decimal CostPerPerson { get; set; }
        /// <summary>
        /// Количество отдыхающих(максимум 5)
        /// </summary>
        public int NumberOfPeople { get; set; }
        /// <summary>
        /// Наличие Wi-Fi
        /// </summary>
        public bool HasWiFi { get; set; }
        /// <summary>
        /// Доплаты (в рублях)
        /// </summary>
        public decimal Surcharges { get; set; }
        /// <summary>
        /// Цена за одну ночь для одного отдыхающего
        /// </summary>
        public decimal PricePerNight => Nights > 0 ? TotalCost / (Nights * NumberOfPeople) : 0;
        /// <summary>
        /// Общая стоимость тура
        /// </summary>
        public decimal TotalCost => ((CostPerPerson * NumberOfPeople) + Surcharges) * Nights;
    }
}
