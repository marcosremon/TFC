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
            if (getUserByEmailRequest == null || string.IsNullOrEmpty(getUserByEmailRequest.Email))
            {
                return BadRequest();
            }

            GetUserByEmailResponse getUserResponse = await _userApplication.GetUserByEmail(getUserByEmailRequest);
            if (getUserResponse.IsSuccess)
            {
                return Ok(getUserResponse);
            }
            return BadRequest(getUserResponse.Message);
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
            if (createUserRequst == null 
                || string.IsNullOrEmpty(createUserRequst.Email) 
                || string.IsNullOrEmpty(createUserRequst.Dni) 
                || string.IsNullOrEmpty(createUserRequst.Username) 
                || string.IsNullOrEmpty(createUserRequst.Password))
            {
                return BadRequest();
            }

            CreateUserResponse response = await _userApplication.CreateUser(createUserRequst);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequst updateUserRequest)
        {
            if (updateUserRequest == null || string.IsNullOrEmpty(updateUserRequest.DniToBeFound) || string.IsNullOrEmpty(updateUserRequest.Email))
            {
                return BadRequest();
            }

            UpdateUserResponse response = await _userApplication.UpdateUser(updateUserRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        {
            if (deleteUserRequest == null || string.IsNullOrEmpty(deleteUserRequest.Dni))
            {
                return BadRequest();
            }

            DeleteUserResponse deleteUsersResponse = await _userApplication.DeleteUser(deleteUserRequest);
            if (deleteUsersResponse.IsSuccess)
            {
                return Ok(deleteUsersResponse);
            }

            return BadRequest(deleteUsersResponse.Message);
        }

        [HttpPost("CreateNewPassword")]
        public async Task<ActionResult<CreateNewPasswordResponse>> CreateNewPassword([FromBody] CreateNewPasswordRequest createNewPasswordRequest)
        {
            if (createNewPasswordRequest == null 
                || string.IsNullOrEmpty(createNewPasswordRequest.UserEmail))
            {
                return BadRequest();
            }

            CreateNewPasswordResponse response = await _userApplication.CreateNewPassword(createNewPasswordRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost("ChangePasswordWithPasswordAndEmail")]
        public async Task<ActionResult<ChangePasswordWithPasswordAndEmailResponse>> ChangePasswordWithPasswordAndEmail([FromBody] ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest)
        {   
            if (changePasswordWithPasswordAndEmailRequest == null 
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.UserEmail)
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.NewPassword)
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.OldPassword)
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.ConfirmNewPassword)
                || changePasswordWithPasswordAndEmailRequest.NewPassword != changePasswordWithPasswordAndEmailRequest.ConfirmNewPassword)
            {
                return BadRequest();
            }

            ChangePasswordWithPasswordAndEmailResponse response = await _userApplication.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
         
            return BadRequest(response.Message);
        }
    }
}