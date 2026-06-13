using HotToursManager.Models;

namespace HotToursManager.Web.ViewModels
{
    /// <summary>
    /// ViewModel для отображения информации о турах на главной странице
    /// </summary>
    public class ToursViewModel
    {

        /// <summary>
        /// Список туров для отображения
        /// </summary>
        public required IEnumerable<Tour> Tours { get; set; }
        /// <summary>
        /// Статистика по турам
        /// </summary>
        public required Statistics Statistics { get; set; }

        /// <summary>
        /// Сообщение об успехе
        /// </summary>
        public string? Message { get; set; }
    }
}
