using ParkingSystem.Data;
using ParkingSystem.ViewModels;

namespace ParkingSystem.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DashboardViewModel GetDashboard()
        {
            return new DashboardViewModel
            {
                // Total kendaraan
                TotalVehicles = _context.Vehicles.Count(),

                // Total petugas
                TotalOfficers = _context.Officers.Count(),

                // Kendaraan yang masih berada di area parkir
                TotalParking = _context.ParkingTransactions
                    .Count(x => x.Status == "Masuk"),

                // Pendapatan hari ini
                TodayIncome = _context.ParkingTransactions
                    .Where(x =>
                        x.ExitTime != null &&
                        x.ExitTime.Value.Date == DateTime.Today)
                    .Sum(x => x.TotalFee)
            };
        }
        
    }
}
