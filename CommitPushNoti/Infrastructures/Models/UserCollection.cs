namespace CommitPushNoti.Infrastructures.Models
{
    public class UserCollection
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
