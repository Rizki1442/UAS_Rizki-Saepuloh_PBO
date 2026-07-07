using Microsoft.EntityFrameworkCore;
using ParkingSystem.Data;
using ParkingSystem.Models;

namespace ParkingSystem.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Semua transaksi
        public List<ParkingTransaction> GetAll()
        {
            return _context.ParkingTransactions
                .Include(x => x.Vehicle)
                .Include(x => x.Officer)
                .OrderByDescending(x => x.EntryTime)
                .ToList();
        }

        // Filter berdasarkan tanggal masuk
        public List<ParkingTransaction> GetByDate(DateTime startDate, DateTime endDate)
        {
            return _context.ParkingTransactions
                .Include(x => x.Vehicle)
                .Include(x => x.Officer)
                .Where(x => x.EntryTime.Date >= startDate.Date &&
                            x.EntryTime.Date <= endDate.Date)
                .OrderByDescending(x => x.EntryTime)
                .ToList();
        }
    }
}