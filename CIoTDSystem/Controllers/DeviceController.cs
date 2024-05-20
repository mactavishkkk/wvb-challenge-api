using CIoTDSystem.Models.DTOs;
using CIoTDSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIoTDSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]

    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetAllDevicesAsync()
        {
            return await _deviceService.GetAllDevicesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetSingleDeviceAsync(int id)
        {
            var result = await _deviceService.GetSingleDeviceAsync(id);
            if (result is null)
            {
                return NotFound("Algo deu errado: GetSingleDeviceAsync();");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> CreateDeviceAsync(DeviceDTO request)
        {
            var result = await _deviceService.CreateDeviceAsync(request);
            if (result is null)
            {
                return NotFound("Algo deu errado: CreateDeviceAsync();");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Device>> UpdateDeviceAsync(int id, DeviceDTO request)
        {
            var result = await _deviceService.UpdateDeviceAsync(id, request);
            if (result is null)
            {
                return NotFound("Algo deu errado: UpdateDeviceAsync();");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Device>> DeleteDeviceAsync(int id)
        {
            var result = await _deviceService.DeleteDeviceAsync(id);
            if (result is null)
            {
                return NotFound("Algo deu errado: DeleteDeviceAsync();");
            }
            return Ok(result);
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<Device>> ChangeStatusAsync(int id)
        {
            var result = await _deviceService.ChangeStatusAsync(id);
            if (result is null)
            {
                return NotFound("Algo deu errado: ChangeStatusAsync();");
            }
            return Ok(result);
        }
    }
}
