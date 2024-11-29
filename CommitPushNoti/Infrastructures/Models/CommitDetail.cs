namespace CommitPushNoti.Infrastructures.Models
{
    public class CommitDetail
    {
        public CommitDetail(string id, string commitMessage, DateTime createDate, string commitUrl, int lineChange, string userEmail, string repositoryId)
        {
            Id            = id;
            //Collection    = Repository.Project.Collection.Name;
            //Project       = Repository.Project.Name;
            CommitMessage = commitMessage;
            CreateDate    = createDate;
            CommitUrl     = commitUrl;
            LineChange    = lineChange;
            UserEmail     = userEmail;
            RepositoryId  = repositoryId;
        }

        public string       Id            { get; set; }
        //public string     Collection    { get; set; }
        //public string     Project       { get; set; }
        public string       CommitMessage { get; set; }
        public DateTime     CreateDate    { get; set; }
        public string       CommitUrl     { get; set; }
        public int          LineChange    { get; set; }
        public string       UserEmail     { get; set; }
        public string       RepositoryId  { get; set; }
        public Repository   Repository    { get; set; }
        public User         User          { get; set; }
    }
}
