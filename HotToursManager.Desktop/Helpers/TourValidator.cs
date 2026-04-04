using HotToursManager.Desktop.Constants;

namespace HotToursManager.Desktop.Helpers
{
    /// <summary>
    /// Класс для валидации данных формы тура.
    /// </summary>
    public static class TourValidator
    {
        /// <summary>
        /// Проверяет корректность данных формы тура.
        /// </summary>
        public static bool ValidateForm(
            string destination,
            int nights,
            decimal costPerPerson,
            int numberOfPeople,
            decimal surcharges,
            DateTime departureDate,
            int? editingId,
            out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(destination))
            {
                errorMessage = "Направление не может быть пустым.";
                return false;
            }

            if (nights < TourLimits.MinNights || nights > TourLimits.MaxNights)
            {
                errorMessage = $"Количество ночей должно быть от {TourLimits.MinNights} до {TourLimits.MaxNights}.";
                return false;
            }

            if (costPerPerson < TourLimits.MinCostPerPerson || costPerPerson > TourLimits.MaxCostPerPerson)
            {
                errorMessage = $"Стоимость должна быть от {TourLimits.MinCostPerPerson:N0} до {TourLimits.MaxCostPerPerson:N0} рублей.";
                return false;
            }

            if (numberOfPeople < TourLimits.MinPeople || numberOfPeople > TourLimits.MaxPeople)
            {
                errorMessage = $"Количество отдыхающих должно быть от {TourLimits.MinPeople} до {TourLimits.MaxPeople}.";
                return false;
            }

            if (surcharges < TourLimits.MinSurcharge || surcharges > TourLimits.MaxSurcharge)
            {
                errorMessage = $"Доплата должна быть от {TourLimits.MinSurcharge:N0} до {TourLimits.MaxSurcharge:N0} рублей.";
                return false;
            }

            if (departureDate.Date < DateTime.Today.Date && editingId.HasValue == false)
            {
                errorMessage = "Дата вылета не может быть в прошлом (для нового тура).";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
