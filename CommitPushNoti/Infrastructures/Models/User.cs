namespace CommitPushNoti.Infrastructures.Models
{
    public class User
    {
        public User(string email, string name)
        {
            Email = email;
            Name  = name;
        }

        public string             Email         { get; set; }
        public string             Name          { get; set; }
        public virtual List<CommitDetail> CommitDetails { get; set; }
        public virtual List<UserProject>  UserProject   { get; set; }
        public virtual List<PullRequest>  PullRequests  { get; set; }
    }
}
