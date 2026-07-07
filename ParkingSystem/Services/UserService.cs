using ParkingSystem.Data;
using ParkingSystem.Models;

namespace ParkingSystem.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? Login(string username, string password)
        {
            return _context.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);
        }
    }
}