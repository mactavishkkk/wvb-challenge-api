using CIoTDSystem.Data;
using CIoTDSystem.Services.Exceptions;
using CIoTDSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CIoTDSystem.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly DataContext _dbContext;

        public DeviceService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            try
            {
                var devices = await _dbContext.Device.ToListAsync();
                return devices;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Device?> GetSingleDeviceAsync(int id)
        {
            try
            {
                var device = await _dbContext.Device.FindAsync(id);
                return device;

            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<Device> CreateDeviceAsync(Device device)
        {
            try
            {
                _dbContext.Device.Add(device);
                await _dbContext.SaveChangesAsync();

                return device;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<Device?> UpdateDeviceAsync(int id, Device request)
        {
            try
            {
                var device = await _dbContext.Device.FindAsync(id);

                if (device is not null)
                {
                    device.Identifier = request.Identifier;
                    device.Description = request.Description;
                    device.Manufacturer = request.Manufacturer;
                    device.Url = request.Url;
                    device.Status = request.Status;
                    device.UpdatedAt = DateTime.UtcNow;
                }
                await _dbContext.SaveChangesAsync();

                return device;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<Device?> DeleteDeviceAsync(int id)
        {
            try
            {
                var device = await _dbContext.Device.FindAsync(id);
                if (device is not null)
                {
                    _dbContext.Device.Remove(device);
                }

                await _dbContext.SaveChangesAsync();

                return device;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<Device?> ChangeStatusAsync(int id)
        {
            try
            {
                var device = await _dbContext.Device.FindAsync(id);

                if (device is not null)
                {
                    device.Status = !device.Status;
                    device.UpdatedAt = DateTime.UtcNow;
                }
                await _dbContext.SaveChangesAsync();

                return device;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
