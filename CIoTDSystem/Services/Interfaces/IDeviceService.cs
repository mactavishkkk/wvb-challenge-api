using CIoTDSystem.Models.DTOs;

namespace CIoTDSystem.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device?> GetSingleDeviceAsync(int id);
        Task<Device> CreateDeviceAsync(DeviceDTO request);
        Task<Device?> UpdateDeviceAsync(int id, DeviceDTO request);
        Task<Device?> DeleteDeviceAsync(int id);
        Task<Device?> ChangeStatusAsync(int id);
    }
}