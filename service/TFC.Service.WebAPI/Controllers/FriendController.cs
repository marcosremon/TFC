using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Friend.AddNewUserFriend;
using TFC.Application.DTO.Friend.DeleteFriend;
using TFC.Application.DTO.Friend.GetAllUserFriens;
using TFC.Application.Interface.Application;
using TFC.Transversal.Logs;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/friend")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendApplication _friendApplication;

        public FriendController(IFriendApplication friendApplication)
        {
            _friendApplication = friendApplication;
        }

        [HttpPost("get-all-user-friends")]
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

        [HttpPost("add-new-user-friend")]
        public async Task<ActionResult<AddNewUserFriendResponse>> AddNewUserFriend([FromBody] AddNewUserFriendRequest addNewUserFriendRequest)
        {
            AddNewUserFriendResponse response = new AddNewUserFriendResponse();
            try
            {
                response = await _friendApplication.AddNewUserFriend(addNewUserFriendRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"se añadio al amigo con id {response.FriendId}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al añadir al amigo con id: {response.IsSuccess}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"AddNewUserFriend --> Error al añadir al amigo con id: {response.FriendId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-friend")]
        public async Task<ActionResult<DeleteFriendResponse>> DeleteFriend([FromBody] DeleteFriendRequest deleteFriendRequest)
        {
            try
            {
                DeleteFriendResponse response = await _friendApplication.DeleteFriend(deleteFriendRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Amigo eliminado con friendCode: {deleteFriendRequest.FriendEmail}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al eliminar el amigo con friendCode: {deleteFriendRequest.FriendEmail}");
                return BadRequest(response.IsSuccess);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"DeleteFriend --> Error al eliminar el amigo con friendCode: {deleteFriendRequest.FriendEmail}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}