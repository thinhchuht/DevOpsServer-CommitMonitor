namespace CommitPushNoti.Infrastructures.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CommitDetail> CommitDetails { get; set; }
        public List<UserCollection> UserCollection { get; set; }
    }
}
