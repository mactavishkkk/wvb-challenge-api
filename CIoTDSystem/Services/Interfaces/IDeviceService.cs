namespace CIoTDSystem.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device?> GetSingleDeviceAsync(int id);
        Task<Device> CreateDeviceAsync(Device device);
        Task<Device?> UpdateDeviceAsync(int id, Device request);
        Task<Device?> DeleteDeviceAsync(int id);
        Task<Device?> ChangeStatusAsync(int id);
    }
}