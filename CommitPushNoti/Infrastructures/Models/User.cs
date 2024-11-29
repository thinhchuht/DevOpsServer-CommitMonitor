namespace CommitPushNoti.Infrastructures.Models
{
    public class User
    {
        public string             Email         { get; set; }
        public string             Name          { get; set; }
        public List<CommitDetail> CommitDetails { get; set; }
        public List<UserProject>  UserProject   { get; set; }
    }
}
