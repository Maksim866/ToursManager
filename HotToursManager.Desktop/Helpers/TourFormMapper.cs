using HotToursManager.Models;

namespace HotToursManager.Desktop.Helpers
{
    public static class TourFormMapper
    {
        public static void LoadTourToForm(Tour tour, TextBox destination, DateTimePicker departure, TextBox nights, TextBox cost, TextBox people, CheckBox wifi, TextBox surcharges)
        {
            destination.Text = tour.Destination;
            departure.Value = tour.DepartureDate;
            nights.Text = tour.Nights.ToString();
            cost.Text = tour.CostPerPerson.ToString();
            people.Text = tour.NumberOfPeople.ToString();
            wifi.Checked = tour.HasWiFi;
            surcharges.Text = tour.Surcharges.ToString();
        }

        public static void SaveFormToTour(Tour tour, string destination, DateTime departure, int nights, decimal cost, int people, bool hasWiFi, decimal surcharges)
        {
            tour.Destination = destination;
            tour.DepartureDate = departure;
            tour.Nights = nights;
            tour.CostPerPerson = cost;
            tour.NumberOfPeople = people;
            tour.HasWiFi = hasWiFi;
            tour.Surcharges = surcharges;
        }
    }
}
