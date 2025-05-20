using TFC.Application.DTO.User.AddNewUserFriend;
using TFC.Application.DTO.User.GetAllUserFriens;
using TFC.Application.DTO.User.GetFriendByFriendCode;

namespace TFC.Application.Interface.Persistence
{
    public interface IFriendRepository
    {
        Task<GetAllUserFriendsResponse> GetAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest);
        Task<AddNewUserFriendResponse> AddNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest);
        Task<GetFriendByFriendCodeResponse> GetFriendByFriendCode(GetFriendByFriendCodeRequest getFriendByFriendCodeRequest);
    }
}
