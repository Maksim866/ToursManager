namespace HotToursManager.Models
{
    /// <summary>
    /// Класс, содержащий статистику по турам
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// Общее количество туров
        /// </summary>
        public int TotalTours { get; set; }

        /// <summary>
        /// Общая стоимость всех туров
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// Количество туров с доплатами
        /// </summary>
        public int ToursWithSurcharges { get; set; }

        /// <summary>
        /// Общая сумма доплат
        /// </summary>
        public decimal TotalSurcharges { get; set; }
    }
}