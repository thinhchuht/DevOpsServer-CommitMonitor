namespace CommitPushNoti.Data
{
    public class PullRequestNotification
    {
        public string               Id                 { get; set; }
        public string               EventType          { get; set; }
        public string               PublisherId        { get; set; }
        public ResourcePR           Resource           { get; set; }
        public string               ResourceVersion    { get; set; }
        public ResourceContainersPR ResourceContainers { get; set; }
        public DateTime             CreatedDate        { get; set; }
    }

    public class ResourcePR
    {
        public RepositoryPR   Repository            { get; set; }
        public int            PullRequestId         { get; set; }
        public string         Status                { get; set; }
        public CreatedBy      CreatedBy             { get; set; }
        public DateTime       CreationDate          { get; set; }
        public string         Title                 { get; set; }
        public string         Description           { get; set; }
        public string         SourceRefName         { get; set; }
        public string         TargetRefName         { get; set; }
        public string         MergeStatus           { get; set; }
        public string         MergeId               { get; set; }
        public Commit         LastMergeSourceCommit { get; set; }
        public Commit         LastMergeTargetCommit { get; set; }
        public Commit         LastMergeCommit       { get; set; }
        public List<Reviewer> Reviewers             { get; set; }
        public List<Commit>   Commits               { get; set; }
        public string         Url                   { get; set; }
        public Links          Links                 { get; set; }
    }

    public class RepositoryPR
    {
        public string  Id            { get; set; }
        public string  Name          { get; set; }
        public string  Url           { get; set; }
        public Project Project       { get; set; }
        public string  DefaultBranch { get; set; }
        public string  RemoteUrl     { get; set; }
    }

    public class CreatedBy
    {
        public string DisplayName { get; set; }
        public string Url         { get; set; }
        public string Id          { get; set; }
        public string UniqueName  { get; set; }
        public string ImageUrl    { get; set; }
        public string Email
        {
            get
            {
                if (!string.IsNullOrEmpty(UniqueName))
                {
                    var userName = UniqueName.Split('\\').Last();
                    return $"{userName}@fpts.com.vn";
                }
                return null;
            }
        }
    }

    public class Reviewer
    {
        public string ReviewerUrl { get; set; }
        public int    Vote        { get; set; }
        public string DisplayName { get; set; }
        public string Url         { get; set; }
        public string Id          { get; set; }
        public string UniqueName  { get; set; }
        public string ImageUrl    { get; set; }
        public bool   IsContainer { get; set; }
    }

    public class Links
    {
        public Web      Web      { get; set; }
        public Statuses Statuses { get; set; }
    }

    public class Web
    {
        public string Href { get; set; }
    }

    public class Statuses
    {
        public string Href { get; set; }
    }

    public class ResourceContainersPR
    {
        public Collection Collection { get; set; }
        public Account    Account    { get; set; }
        public Project    Project    { get; set; }
    }

    public class Account
    {
        public string Id { get; set; }
    }
}