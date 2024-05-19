using CIoTDSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIoTDSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUserAsync(int id)
        {
            var result = await _userService.GetSingleUserAsync(id);
            if (result is null)
            {
                return NotFound("Algo deu errado: GetSingleUserAsync();");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUserAsync(User user)
        {
            var result = await _userService.CreateUserAsync(user);
            if (result is null)
            {
                return NotFound("Algo deu errado: CreateUserAsync();");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUserAsync(int id, User request)
        {
            var result = await _userService.UpdateUserAsync(id, request);
            if (result is null)
            {
                return NotFound("Algo deu errado: UpdateUserAsync();");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result is null)
            {
                return NotFound("Algo deu errado: DeleteUserAsync();");
            }
            return Ok(result);
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<User>> ChangeStatusAsync(int id)
        {
            var result = await _userService.ChangeStatusAsync(id);
            if (result is null)
            {
                return NotFound("Algo deu errado: ChangeStatusAsync();");
            }
            return Ok(result);
        }
    }
}
