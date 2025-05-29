namespace TFC.Domain.Model.Entity
{
    public class UserFriend
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FriendId { get; set; }
    }
}