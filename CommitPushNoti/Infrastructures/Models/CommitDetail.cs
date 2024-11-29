namespace CommitPushNoti.Infrastructures.Models
{
    public class CommitDetail
    {
        public string Id { get; set; }
        public string Collection { get; set; }
        public string Project { get; set; }
        public string CommitMessage { get; set; }
        public string CreateDate { get; set; }
        public string CommitUrl { get; set; }
        public int LineChange { get; set; }
        public string UserId { get; set; }
        public string RepositoryId { get; set; }
        public Repository Repository { get; set; }
        public User User { get; set; }
    }
}
