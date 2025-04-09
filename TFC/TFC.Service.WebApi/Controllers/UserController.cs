using Kintech.RestCA.Transversal.Common;
using Kintech.WebServices.Applicacion.DTO.CreateLicense;
using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Request;
using TFC.Application.DTO.Response;
using TFC.Application.Interface;
using TFC.Domain.Model.Entity;
using TFC.Transversal.Mail;

namespace TFC.Service.WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet]
        public async Task<ActionResult<GetUserByEmailResponse>> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            GetUserByEmailResponse getUserResponse = await _userApplication.GetUserByEmail(email);
            if (getUserResponse.IsSuccess)
            {
                return Ok(getUserResponse);
            }
            return BadRequest(getUserResponse.Message);
        }

        [HttpGet]
        public async Task<ActionResult<GetUsersResponse>> GetUsers()
        {
            GetUsersResponse getUsersResponse = await _userApplication.GetUsers();
            if (getUsersResponse.IsSuccess)
            {
                return Ok(getUsersResponse);
            }

            return BadRequest(getUsersResponse.Message);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] UserDTO importUser)
        {
            CreateUserRequst createUserRequst = new CreateUserRequst()
            {
                Dni = importUser.Dni,
                Username = importUser.Username,
                Surname = importUser.Surname,
                Email = importUser.Email,
                Password = importUser.Password
            };

            CreateUserResponse createUserResponse = await _userApplication.CreateUser(createUserRequst);
            if (createUserResponse.IsSuccess)
            {
                return Ok(createUserResponse);
            }

            return BadRequest(createUserResponse.Message);
        }

        [HttpPut]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequst request)
        {
            UpdateUserResponse updateUserResponse = await _userApplication.UpdateUser(request);
            if (updateUserResponse.IsSuccess)
            {
                return Ok(updateUserResponse);
            }

            return BadRequest(updateUserResponse.Message);
        }

        [HttpDelete("{UserId}")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser(long UserId)
        {
            DeleteUserResponse deleteUsersResponse = await _userApplication.DeleteUser(UserId);
            if (deleteUsersResponse.IsSuccess)
            {
                return Ok(deleteUsersResponse);
            }

            return BadRequest(deleteUsersResponse.Message);
        }

        [HttpPost("CreateNewPassword")]
        public async Task<ActionResult<CreateNewPasswordResponse>> CreateNewPassword([FromBody] CreateNewPasswordRequest createNewPasswordRequest)
        {
            if (createNewPasswordRequest == null)
            {
                return BadRequest();
            }

            CreateNewPasswordResponse response = await _userApplication.CreateNewPassword(createNewPasswordRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
