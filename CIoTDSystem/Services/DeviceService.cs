using CIoTDSystem.Data;
using CIoTDSystem.Models.DTOs;
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
                var devices = await _dbContext.Device
                    .Include(x => x.Commands)
                    .ToListAsync();

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
                var device = await _dbContext.Device
                    .Include(x => x.Commands)
                    .SingleOrDefaultAsync(p => p.Id == id);

                return device;

            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<Device> CreateDeviceAsync(DeviceDTO request)
        {
            try
            {
                var newDevice = new Device
                {
                    Identifier = request.Identifier,
                    Description = request.Description,
                    Manufacturer = request.Manufacturer,
                    Url = request.Url,
                    Status = request.Status,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var commands = request.Commands.Select(x => new Command
                {
                    Name = x.Name,
                    Description = x.Description,
                    Parameters = x.Parameters,
                    ReturnType = x.ReturnType
                }).ToList();

                newDevice.Commands = commands;

                _dbContext.Device.Add(newDevice);
                await _dbContext.SaveChangesAsync();

                return newDevice;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<Device?> UpdateDeviceAsync(int id, DeviceDTO request)
        {
            try
            {
                var device = await _dbContext.Device
                    .Include(d => d.Commands)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (device is not null)
                {
                    _dbContext.Commands.RemoveRange(device.Commands);

                    var commands = request.Commands.Select(x => new Command
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Parameters = x.Parameters,
                        ReturnType = x.ReturnType
                    }).ToList();

                    device.Commands = commands;

                    device.Identifier = request.Identifier;
                    device.Description = request.Description;
                    device.Manufacturer = request.Manufacturer;
                    device.Url = request.Url;
                    device.Status = request.Status;
                    device.UpdatedAt = DateTime.UtcNow;
                    _dbContext.Device.Update(device);
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
