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
            TextBox destinationBox,
            TextBox nightsBox,
            TextBox costBox,
            TextBox peopleBox,
            TextBox surchargesBox,
            DateTimePicker datePicker,
            out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(destination))
            {
                errorMessage = "Заполните направление";
                destinationBox.Focus();
                return false;
            }

            if (nights < TourLimits.MinNights || nights > TourLimits.MaxNights)
            {
                errorMessage = "Количество ночей должно быть от 1 до 99";
                nightsBox.Focus();
                return false;
            }

            if (costPerPerson < TourLimits.MinCostPerPerson || costPerPerson > TourLimits.MaxCostPerPerson)
            {
                errorMessage = "Стоимость должна быть от 1000 до 500000 рублей";
                costBox.Focus();
                return false;
            }

            if (numberOfPeople < TourLimits.MinPeople || numberOfPeople > TourLimits.MaxPeople)
            {
                errorMessage = "Количество отдыхающих должно быть от 1 до 5";
                peopleBox.Focus();
                return false;
            }

            if (surcharges < TourLimits.MinSurcharge || surcharges > TourLimits.MaxSurcharge)
            {
                errorMessage = "Доплата должна быть от 0 до 100000 рублей";
                surchargesBox.Focus();
                return false;
            }

            if (departureDate.Date < DateTime.Today.Date && editingId.HasValue == false)
            {
                errorMessage = "Дата вылета не может быть в прошлом";
                datePicker.Focus();
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
