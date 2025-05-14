using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;
using TFC.Application.Interface.Application;
using TFC.Transversal.Logs;

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
                    Log.Instance.Trace($"Usuario encontrado con email: {getUserByEmailRequest.Email}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al buscar el usuario por email: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetUserByEmail --> Error al buscar el usuario por email: {ex.Message}");
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
                    Log.Instance.Trace($"Usuarios encontrados: {response.Users?.Count}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al buscar los usuarios: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetUsers --> Error al buscar los usuarios: {ex.Message}");
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
                    Log.Instance.Trace($"Usuario creado correctamente con email: {createUserRequst.Email}");
                    return Created(string.Empty, response);
                }

                Log.Instance.Trace($"Error al crear el usuario: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"CreateUser --> Error al crear el usuario: {ex.Message}");
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
                    Log.Instance.Trace($"Usuario actualizado correctamente con email: {updateUserRequest.Email}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al actualizar el usuario: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"UpdateUser --> Error al actualizar el usuario: {ex.Message}");
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
                    Log.Instance.Trace($"Usuario eliminado correctamente con email: {deleteUserRequest.Email}");
                    return NoContent();
                }

                Log.Instance.Trace($"Error al eliminar el usuario: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"DeleteUser --> Error al eliminar el usuario: {ex.Message}");
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
                    Log.Instance.Trace($"Nueva contraseña creada correctamente para el usuario con email: {createNewPasswordRequest.Email}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al crear la nueva contraseña: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"CreateNewPassword --> Error al crear la nueva contraseña: {ex.Message}");
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
                    Log.Instance.Trace($"Contraseña cambiada correctamente para el usuario con email: {changePasswordWithPasswordAndEmailRequest.Email}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al cambiar la contraseña: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"ChangePasswordWithPasswordAndEmail --> Error al cambiar la contraseña: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}