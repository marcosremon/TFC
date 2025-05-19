using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.User.AddNewUserFriend;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateAdmin;
using TFC.Application.DTO.User.CreateGenericUser;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetAllUserFriens;
using TFC.Application.DTO.User.GetFriendByFriendCode;
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

        [HttpGet("GetUsers")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<GetUsersResponse>> GetUsers()
        {
            try
            {
                GetUsersResponse response = await _userApplication.GetUsers();
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Usuarios encontrados: {response.UsersDTO?.Count}");
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
                CreateGenericUserRequest createGenericUserRequest = new CreateGenericUserRequest
                {
                    Dni = createUserRequst.Dni,
                    Username = createUserRequst.Username,
                    Surname = createUserRequst.Surname,
                    Email = createUserRequst.Email,
                    Password = createUserRequst.Password,
                    ConfirmPassword = createUserRequst.ConfirmPassword,
                    Role = Domain.Model.Enum.Role.User
                };

                CreateUserResponse response = await _userApplication.CreateUser(createGenericUserRequest);
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

        [HttpPost("CreateAdmin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<CreateAdminResponse>> CreateAdmin([FromBody] CreateAdminRequest createAdminRequst)
        {
            try
            {
                CreateGenericUserRequest createGenericUserRequest = new CreateGenericUserRequest
                {
                    Dni = createAdminRequst.Dni,
                    Username = createAdminRequst.Username,
                    Surname = createAdminRequst.Surname,
                    Email = createAdminRequst.Email,
                    Password = createAdminRequst.Password,
                    ConfirmPassword = createAdminRequst.ConfirmPassword,
                    Role = Domain.Model.Enum.Role.Admin
                };

                CreateUserResponse response = await _userApplication.CreateUser(createGenericUserRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Admin creado correctamente con email: {createGenericUserRequest.Email}");
                    return Created(string.Empty, response);
                }

                Log.Instance.Trace($"Error al crear el admin: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"CreateAdmin --> Error al crear el admin: {ex.Message}");
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
                    Log.Instance.Trace($"Usuario eliminado correctamente con DNI: {deleteUserRequest.Dni}");
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

        [HttpPost("GetAllUserFriends")]
        public async Task<ActionResult<GetAllUserFriendsResponse>> GetAllUserFriends([FromBody] GetAllUserFriendsRequest getAllUserFriendsRequest)
        {
            try
            {
                GetAllUserFriendsResponse response = await _userApplication.GetAllUserFriends(getAllUserFriendsRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Amigos encontrados: {response.Friends?.Count}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al buscar los amigos: {response?.Message}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetAllUserFriends --> Error al buscar los amigos: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewUserFriend")]
        public async Task<ActionResult<AddNewUserFriendResponse>> AddNewUserFriend([FromBody] AddNewUserFriendRequest addNewUserFriendRequest)
        {
            AddNewUserFriendResponse response = new AddNewUserFriendResponse();
            try
            {
                response = await _userApplication.AddNewUserFriend(addNewUserFriendRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"se añadio al amigo con id {response.FriendId}");
                    return Ok(response.IsSuccess);
                }

                Log.Instance.Trace($"Error al añadir al amigo con id: {response.IsSuccess}");
                return BadRequest(response.IsSuccess);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"AddNewUserFriend --> Error al añadir al amigo con id: {response.FriendId}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("GetFriendByFriendCode")]
        public async Task<ActionResult<GetFriendByFriendCodeResponse>> GetFriendByFriendCode([FromQuery] GetFriendByFriendCodeRequest getFriendByFriendCodeRequest)
        {
            GetFriendByFriendCodeResponse response = new GetFriendByFriendCodeResponse();
            try
            {
                response = await _userApplication.GetFriendByFriendCode(getFriendByFriendCodeRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Amigo encontrado con código: {getFriendByFriendCodeRequest.FriendCode}");
                    return Ok(response);
                }

                Log.Instance.Trace($"No se encontró amigo con código: {getFriendByFriendCodeRequest.FriendCode}");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetFriendByFriendCode --> Error al buscar amigo con código {getFriendByFriendCodeRequest.FriendCode}: {ex.Message}");
                return BadRequest();
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
                    Log.Instance.Trace($"Nueva contraseña creada correctamente para el usuario con email: {createNewPasswordRequest.UserEmail}");
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
                    Log.Instance.Trace($"Contraseña cambiada correctamente para el usuario con email: {changePasswordWithPasswordAndEmailRequest.UserEmail}");
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