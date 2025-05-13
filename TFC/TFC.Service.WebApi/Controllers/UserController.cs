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
            try
            {
                GetUserByEmailResponse getUserByEmailResponse = await _userApplication.GetUserByEmail(getUserByEmailRequest);
                if (getUserByEmailResponse.IsSuccess)
                {
                    return BadRequest(getUserByEmailResponse);
                }

                return BadRequest(getUserByEmailResponse.Message);
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
                GetUsersResponse getUsersResponse = await _userApplication.GetUsers();
                if (getUsersResponse.IsSuccess)
                {
                    return Ok(getUsersResponse);
                }

                return BadRequest(getUsersResponse.Message);
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
                CreateUserResponse createUserResponse = await _userApplication.CreateUser(createUserRequst);
                if (createUserResponse.IsSuccess)
                {
                    return Ok(createUserResponse);
                }

                return BadRequest(createUserResponse.Message);
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
                UpdateUserResponse updateUserResponse = await _userApplication.UpdateUser(updateUserRequest);
                if (updateUserResponse.IsSuccess)
                {
                    return Ok(updateUserResponse);
                }

                return BadRequest(updateUserResponse.Message);
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
                DeleteUserResponse deleteUserResponse = await _userApplication.DeleteUser(deleteUserRequest);
                if (deleteUserResponse.IsSuccess)
                {
                    return Ok(deleteUserResponse);
                }

                return BadRequest(deleteUserResponse.Message);
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
                CreateNewPasswordResponse createNewPasswordResponse = await _userApplication.CreateNewPassword(createNewPasswordRequest);
                if (createNewPasswordResponse.IsSuccess)
                {
                    return Ok(createNewPasswordResponse);
                }

                return BadRequest(createNewPasswordResponse.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ChangePasswordWithPasswordAndEmail")]
        public async Task<ActionResult<ChangePasswordWithPasswordAndEmailResponse>> ChangePasswordWithPasswordAndEmail([FromBody] ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest)
        {
            ChangePasswordWithPasswordAndEmailResponse changePasswordWithPasswordAndEmailResponse = await _userApplication.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
            if (changePasswordWithPasswordAndEmailResponse.IsSuccess)
            {
                return BadRequest(changePasswordWithPasswordAndEmailResponse.Message);
            }

            return Ok(changePasswordWithPasswordAndEmailResponse);
        }
    }
}