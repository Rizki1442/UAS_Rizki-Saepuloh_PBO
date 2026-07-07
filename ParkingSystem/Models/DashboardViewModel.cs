namespace ParkingSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalVehicles { get; set; }

        public int TotalOfficers { get; set; }
        public int TotalParking { get; set; }

        public int TodayTransaction { get; set; }

        public decimal TodayIncome { get; set; }

        public decimal MonthlyIncome { get; set; }
    }
}