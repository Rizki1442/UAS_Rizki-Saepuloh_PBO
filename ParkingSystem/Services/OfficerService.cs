using ParkingSystem.Data;
using ParkingSystem.Models;

namespace ParkingSystem.Services
{
    public class OfficerService
    {
        private readonly ApplicationDbContext _context;

        public OfficerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Officer> GetAll()
        {
            return _context.Officers
                .OrderBy(x => x.Name)
                .ToList();
        }

        public Officer? GetById(Guid id)
        {
            return _context.Officers
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Officer officer)
        {
            _context.Officers.Add(officer);
            _context.SaveChanges();
        }

        public void Update(Officer officer)
        {
            _context.Officers.Update(officer);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var officer = GetById(id);

            if (officer != null)
            {
                _context.Officers.Remove(officer);
                _context.SaveChanges();
            }
        }

        public List<Officer> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            keyword = keyword.ToLower();

            return _context.Officers
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Phone.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}