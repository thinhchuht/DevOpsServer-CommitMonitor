namespace CommitPushNoti.Infrastructures.Models
{
    public class PullRequest
    {
        public PullRequest(int id, string status, DateTime createdDate, string title, string description, string url, string userEmail, string repositoryId)
        {
            Id           = id;
            Status       = status;
            CreatedDate  = createdDate;
            Title        = title;
            Description  = description;
            Url          = url;
            UserEmail    = userEmail;
            RepositoryId = repositoryId;
        }

        public int                Id            { get; set; }
        public string             Status        { get; set; }
        public DateTime           CreatedDate   { get; set; }
        public string             Title         { get; set; }
        public string             Description   { get; set; }
        public string             Url           { get; set; }
        public string             UserEmail     { get; set; }
        public virtual User User { get; set; }
        public string RepositoryId  { get; set; }
        public virtual Repository         Repository    { get; set; }
        public virtual List<CommitDetail> CommitDetails { get; set; }

    }
}
