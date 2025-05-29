using TFC.Application.DTO.Friend.AddNewUserFriend;
using TFC.Application.DTO.Friend.DeleteFriend;
using TFC.Application.DTO.Friend.GetAllUserFriens;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;
using TFC.Transversal.Logs;

namespace TFC.Application.Main
{
    public class FriendApplication : IFriendApplication
    {
        private readonly IFriendRepository _friendRepository;
        public FriendApplication(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<GetAllUserFriendsResponse> GetAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest)
        {
            if (getAllUserFriendsRequest == null || string.IsNullOrEmpty(getAllUserFriendsRequest.UserEmail))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new GetAllUserFriendsResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetAllUserFriendsRequest is null or UserEmail is missing."
                };
            }

            return await _friendRepository.GetAllUserFriends(getAllUserFriendsRequest);
        }

        public async Task<AddNewUserFriendResponse> AddNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest)
        {
            if (addNewUserFriendRequest == null
                || string.IsNullOrEmpty(addNewUserFriendRequest.UserEmail)
                || string.IsNullOrEmpty(addNewUserFriendRequest.FriendCode))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new AddNewUserFriendResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: AddNewUserFriendRequest is null or required fields are missing."
                };
            }

            return await _friendRepository.AddNewUserFriend(addNewUserFriendRequest);
        }

        public async Task<DeleteFriendResponse> DeleteFriend(DeleteFriendRequest deleteFriendRequest)
        {
            if (deleteFriendRequest == null
                || string.IsNullOrEmpty(deleteFriendRequest.UserEmail)
                || string.IsNullOrEmpty(deleteFriendRequest.FriendEmail))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new DeleteFriendResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteFriendRequest is null or required fields are missing."
                };
            }

            return await _friendRepository.DeleteFriend(deleteFriendRequest);
        }
    }
}