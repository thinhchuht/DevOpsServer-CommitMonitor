namespace CommitPushNoti.Infrastructures.Models
{
    public class UserProject
    {
        public Guid    Id        { get; set; }
        public string  UserEmail { get; set; }
        public User    User      { get; set; }
        public string  ProjectId { get; set; }
        public Project Project   { get; set; }
    }
}
