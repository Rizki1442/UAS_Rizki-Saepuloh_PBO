using Microsoft.EntityFrameworkCore;
using ParkingSystem.Data;
using ParkingSystem.Models;

namespace ParkingSystem.Services
{
    public class VehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Vehicle> GetAll()
        {
            return _context.Vehicles
                .OrderBy(v => v.PlateNumber)
                .ToList();
        }

        public Vehicle? GetById(Guid id)
        {
            return _context.Vehicles.FirstOrDefault(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);

            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }

        public List<Vehicle> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            keyword = keyword.ToLower();

            return _context.Vehicles
                .Where(v =>
                    v.PlateNumber.ToLower().Contains(keyword) ||
                    v.OwnerName.ToLower().Contains(keyword))
                .OrderBy(v => v.PlateNumber)
                .ToList();
        }
    }
}