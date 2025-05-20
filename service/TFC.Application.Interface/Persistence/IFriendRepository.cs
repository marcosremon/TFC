using TFC.Application.DTO.Friend.AddNewUserFriend;
using TFC.Application.DTO.Friend.DeleteFriend;
using TFC.Application.DTO.Friend.GetAllUserFriens;
using TFC.Application.DTO.Friend.GetFriendByFriendCode;

namespace TFC.Application.Interface.Persistence
{
    public interface IFriendRepository
    {
        Task<GetAllUserFriendsResponse> GetAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest);
        Task<AddNewUserFriendResponse> AddNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest);
        Task<GetFriendByFriendCodeResponse> GetFriendByFriendCode(GetFriendByFriendCodeRequest getFriendByFriendCodeRequest);
        Task<DeleteFriendResponse> DeleteFriend(DeleteFriendRequest deleteFriendRequest);
    }
}
