using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.Interface;

namespace TFC.Service.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(long userId)
        {
            UserDTO user = await _userApplication.GetUserById(userId);

            if (user == null)
            {
                return BadRequest();
            }

           return Ok(user);
        }
    }
}