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
        public Project            Project       { get; set; }
        public List<CommitDetail> CommitDetails { get; set; }
    }
}
