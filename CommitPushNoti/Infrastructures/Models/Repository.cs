namespace CommitPushNoti.Infrastructures.Models
{
    public class Repository
    {
        public Repository(string id, string name, string url, string remoteUrl, string projectId)
        {
            Id        = id;
            Name      = name;
            Url       = url;
            RemoteUrl = remoteUrl;
            ProjectId = projectId;
        }

        public string             Id            { get; set; }
        public string             Name          { get; set; }
        public string             Url           { get; set; }
        public string             RemoteUrl     { get; set; }
        public string             ProjectId     { get; set; }
        public virtual Project            Project       { get; set; }
        public virtual List<CommitDetail> CommitDetails { get; set; }
        public virtual List<PullRequest>  PullRequests  { get; set; }
    }
}
