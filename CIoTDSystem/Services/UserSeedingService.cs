using CIoTDSystem.Data;

namespace CIoTDSystem.Services
{
    public class UserSeedingService
    {
        private DataContext _context;

        public UserSeedingService(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.User.Any())
            {
                return; // Database has been seeded
            }

            User user = new User
            {
                FirstName = "Marcus", LastName = "Silva", Email = "marcus@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), IsAdmin = false, Status = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
            };
            User userTwo = new User
            {
                FirstName = "Anderson", LastName = "Cunha", Email = "andersongmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), IsAdmin = false, Status = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
            };
            User userThree = new User
            {
                FirstName = "Maraiza", LastName = "Onete", Email = "maraiza@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), IsAdmin = true, Status = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
            };
            User userFour = new User
            {
                FirstName = "Ionete", LastName = "Onete", Email = "onete@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), IsAdmin = true, Status = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
            };
            User userFive = new User
            {
                FirstName = "Francisco", LastName = "Souto", Email = "francisco@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"), IsAdmin = false, Status = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
            };

            _context.User.AddRange(user, userTwo, userThree, userFour, userFive);
            _context.SaveChanges();
        }
    }
}