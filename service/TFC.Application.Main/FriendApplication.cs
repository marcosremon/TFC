using TFC.Application.DTO.User.AddNewUserFriend;
using TFC.Application.DTO.User.GetAllUserFriens;
using TFC.Application.DTO.User.GetFriendByFriendCode;
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

        public async Task<GetFriendByFriendCodeResponse> GetFriendByFriendCode(GetFriendByFriendCodeRequest getFriendByFriendCodeRequest)
        {
            if (getFriendByFriendCodeRequest == null
                || string.IsNullOrEmpty(getFriendByFriendCodeRequest.FriendCode))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new GetFriendByFriendCodeResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: friendCode is null or empty."
                };
            }

            return await _friendRepository.GetFriendByFriendCode(getFriendByFriendCodeRequest);
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
    }
}
