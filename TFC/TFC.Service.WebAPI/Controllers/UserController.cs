using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;
using TFC.Application.Interface.Application;

namespace TFC.Service.WebApi.Controllers
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

        [HttpPost("GetUserByEmail")]
        public async Task<ActionResult<GetUserByEmailResponse>> GetUserByEmail([FromBody] GetUserByEmailRequest getUserByEmailRequest)
        {
            try
            {
                GetUserByEmailResponse response = await _userApplication.GetUserByEmail(getUserByEmailRequest);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<GetUsersResponse>> GetUsers()
        {
            try
            {
                GetUsersResponse response = await _userApplication.GetUsers();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequst createUserRequst)
        {
            try
            {
                CreateUserResponse response = await _userApplication.CreateUser(createUserRequst);
                if (response.IsSuccess)
                {
                    return Created(string.Empty, response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequst updateUserRequest)
        {
            try
            {
                UpdateUserResponse response = await _userApplication.UpdateUser(updateUserRequest);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        {
            try
            {
                DeleteUserResponse response = await _userApplication.DeleteUser(deleteUserRequest);
                if (response.IsSuccess)
                {
                    return NoContent();
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateNewPassword")]
        public async Task<ActionResult<CreateNewPasswordResponse>> CreateNewPassword([FromBody] CreateNewPasswordRequest createNewPasswordRequest)
        {
            try
            {
                CreateNewPasswordResponse response = await _userApplication.CreateNewPassword(createNewPasswordRequest);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ChangePasswordWithPasswordAndEmail")]
        public async Task<ActionResult<ChangePasswordWithPasswordAndEmailResponse>> ChangePasswordWithPasswordAndEmail([FromBody] ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest)
        {
            try
            {
                ChangePasswordWithPasswordAndEmailResponse response = await _userApplication.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}