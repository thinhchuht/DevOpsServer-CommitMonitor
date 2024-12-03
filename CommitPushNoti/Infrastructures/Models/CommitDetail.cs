namespace CommitPushNoti.Infrastructures.Models
{
    public class CommitDetail
    {
        public CommitDetail(string id, string commitMessage, DateTime createDate, string commitUrl, int lineChange, string userEmail, string repositoryId)
        {
            Id            = id;
            CommitMessage = commitMessage;
            CreateDate    = createDate;
            CommitUrl     = commitUrl;
            LineChange    = lineChange;
            UserEmail     = userEmail;
            RepositoryId  = repositoryId;
        }

        public string        Id            { get; set; }
        public string        CommitMessage { get; set; }
        public DateTime      CreateDate    { get; set; }
        public string        CommitUrl     { get; set; }
        public int           LineChange    { get; set; }
        public string        UserEmail     { get; set; }
        public string        RepositoryId  { get; set; }
        public virtual Repository    Repository    { get; set; }
        public virtual User          User          { get; set; }
        public int?          PullRequestId { get; set; }
        public virtual PullRequest?  PullRequest   { get; set; }
    }
}
