using CIoTDSystem.Data;

namespace CIoTDSystem.Services.Seedings
{
    public class DeviceSeedingService
    {
        private DataContext _context;

        public DeviceSeedingService(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Device.Any())
            {
                return; // Database has been seeded
            }

            Device device = new Device
            {
                Identifier = "KDLAOIJ",
                Description = "Sensor de temperatura ambiente interno",
                Manufacturer = "Acme Electronics",
                Url = "api/v1/device/KDLAOIJ",
                Status = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            Device deviceOne = new Device
            {
                Identifier = "LDOAIJ",
                Description = "Monitor de umidade do solo",
                Manufacturer = "Beta Technologies",
                Url = "api/v1/device/LDOAIJ",
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            Device deviceTwo = new Device
            {
                Identifier = "ASDFGHJ",
                Description = "Controlador de irrigação",
                Manufacturer = "Gamma Solutions",
                Url = "api/v1/device/ASDFGHJ",
                Status = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Device.AddRange(device, deviceOne, deviceTwo);
            _context.SaveChanges();
        }
    }
}
