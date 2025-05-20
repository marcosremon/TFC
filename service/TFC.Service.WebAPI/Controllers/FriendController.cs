using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.User.AddNewUserFriend;
using TFC.Application.DTO.User.GetAllUserFriens;
using TFC.Application.DTO.User.GetFriendByFriendCode;
using TFC.Application.Interface.Application;
using TFC.Application.Main;
using TFC.Transversal.Logs;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendApplication _friendApplication;

        public FriendController(IFriendApplication friendApplication)
        {
            _friendApplication = friendApplication;
        }


        [HttpPost("GetAllUserFriends")]
        public async Task<ActionResult<GetAllUserFriendsResponse>> GetAllUserFriends([FromBody] GetAllUserFriendsRequest getAllUserFriendsRequest)
        {
            try
            {
                GetAllUserFriendsResponse response = await _friendApplication.GetAllUserFriends(getAllUserFriendsRequest);
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
                response = await _friendApplication.AddNewUserFriend(addNewUserFriendRequest);
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
                response = await _friendApplication.GetFriendByFriendCode(getFriendByFriendCodeRequest);
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
    }
}
