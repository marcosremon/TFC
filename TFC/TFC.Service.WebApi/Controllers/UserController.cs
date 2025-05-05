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
    public class UserController : Controller
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpPost("GetUserByEmail")]
        public async Task<ActionResult<GetUserByEmailResponse>> GetUserByEmail([FromBody] GetUserByEmailRequest getUserByEmailRequest)
        {
            GetUserByEmailResponse response = await _userApplication.GetUserByEmail(getUserByEmailRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<GetUsersResponse>> GetUsers()
        {
            GetUsersResponse response = await _userApplication.GetUsers();
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequst createUserRequst)
        {
            CreateUserResponse response = await _userApplication.CreateUser(createUserRequst);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequst updateUserRequest)
        {
            UpdateUserResponse response = await _userApplication.UpdateUser(updateUserRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        {
            DeleteUserResponse response = await _userApplication.DeleteUser(deleteUserRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("CreateNewPassword")]
        public async Task<ActionResult<CreateNewPasswordResponse>> CreateNewPassword([FromBody] CreateNewPasswordRequest createNewPasswordRequest)
        {
            CreateNewPasswordResponse response = await _userApplication.CreateNewPassword(createNewPasswordRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("ChangePasswordWithPasswordAndEmail")]
        public async Task<ActionResult<ChangePasswordWithPasswordAndEmailResponse>> ChangePasswordWithPasswordAndEmail([FromBody] ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest)
        {   
            ChangePasswordWithPasswordAndEmailResponse response = await _userApplication.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }
         
            return Ok(response);
        }
    }
}