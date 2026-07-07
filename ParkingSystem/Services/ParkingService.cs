using Microsoft.EntityFrameworkCore;
using ParkingSystem.Data;
using ParkingSystem.Models;

namespace ParkingSystem.Services
{
    public class ParkingService
    {
        private readonly ApplicationDbContext _context;

        public ParkingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Menampilkan seluruh transaksi
        public List<ParkingTransaction> GetAll()
        {
            return _context.ParkingTransactions
                .Include(x => x.Vehicle)
                .Include(x => x.Officer)
                .OrderByDescending(x => x.EntryTime)
                .ToList();
        }

        // Kendaraan yang sedang parkir
        public List<ParkingTransaction> GetActiveParking()
        {
            return _context.ParkingTransactions
                .Include(x => x.Vehicle)
                .Include(x => x.Officer)
                .Where(x => x.Status == "Masuk")
                .OrderByDescending(x => x.EntryTime)
                .ToList();
        }

        public ParkingTransaction? GetById(Guid id)
        {
            return _context.ParkingTransactions
                .Include(x => x.Vehicle)
                .Include(x => x.Officer)
                .FirstOrDefault(x => x.Id == id);
        }

        // Kendaraan Masuk
        public void CheckIn(ParkingTransaction transaction)
        {
            // Cek apakah kendaraan masih berada di area parkir
            var activeParking = _context.ParkingTransactions
                .FirstOrDefault(x =>
                    x.VehicleId == transaction.VehicleId &&
                    x.Status == "Masuk");

            if (activeParking != null)
            {
                throw new Exception("Kendaraan masih berada di area parkir.");
            }

            transaction.EntryTime = DateTime.Now;
            transaction.Status = "Masuk";

            _context.ParkingTransactions.Add(transaction);
            _context.SaveChanges();
        }

        // Kendaraan Keluar
        public void CheckOut(Guid id)
        {
            var transaction = _context.ParkingTransactions
                .Include(x => x.Vehicle)
                .FirstOrDefault(x => x.Id == id);

            if (transaction == null)
                return;

            transaction.ExitTime = DateTime.Now;

            transaction.Duration =
                (int)Math.Ceiling(
                    (transaction.ExitTime.Value - transaction.EntryTime).TotalHours);

            if (transaction.Duration <= 0)
                transaction.Duration = 1;

            if (transaction.Vehicle!.VehicleType == "Motor")
                transaction.Rate = 2000;
            else
                transaction.Rate = 5000;

            transaction.TotalFee =
                transaction.Duration * transaction.Rate;

            transaction.Status = "Keluar";

            _context.SaveChanges();
        }
    }
}