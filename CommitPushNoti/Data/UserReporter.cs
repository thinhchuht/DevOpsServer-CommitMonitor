namespace CommitPushNoti.Data
{
    public class UserReporter
    {
        public UserReporter(string username, string email, int pRCount, int commitCount, int lineCount)
        {
            Username    = username;
            Email       = email;
            PRCount     = pRCount;
            CommitCount = commitCount;
            LineCount   = lineCount;
        }

        public string Username    { get; set; }
        public string Email       { get; set; }
        public int    PRCount     { get; set; }
        public int    CommitCount { get; set; }
        public int    LineCount   { get; set; }
    }
}
