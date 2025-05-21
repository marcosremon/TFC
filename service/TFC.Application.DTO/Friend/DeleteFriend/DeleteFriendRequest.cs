namespace TFC.Application.DTO.Friend.DeleteFriend
{
    public class DeleteFriendRequest
    {
        public string UserEmail { get; set; } = string.Empty;
        public string FriendEmail { get; set; } = string.Empty;
    }
}
