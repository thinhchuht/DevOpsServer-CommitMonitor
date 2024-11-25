namespace CommitPushNoti.Data
{
    public class CommitNotification
    {
        public string SubscriptionId { get; set; }
        public int NotificationId { get; set; }
        public string Id { get; set; }
        public string EventType { get; set; }
        public string PublisherId { get; set; }
        public Message Message { get; set; }
        public DetailedMessage DetailedMessage { get; set; }
        public Resource Resource { get; set; }
        public string ResourceVersion { get; set; }
        public ResourceContainers ResourceContainers { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Message
    {
        public string Text { get; set; }
        public string Html { get; set; }
        public string Markdown { get; set; }
    }

    public class DetailedMessage
    {
        public string Text { get; set; }
        public string Html { get; set; }
        public string Markdown { get; set; }
    }

    public class Resource
    {
        public List<Commit> Commits { get; set; }
        public List<RefUpdate> RefUpdates { get; set; }
        public Repository Repository { get; set; }
        public PushedBy PushedBy { get; set; }
        public int PushId { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }

    public class Commit
    {
        public string CommitId { get; set; }
        public Author Author { get; set; }
        public Committer Committer { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class Committer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class RefUpdate
    {
        public string Name { get; set; }
        public string OldObjectId { get; set; }
        public string NewObjectId { get; set; }
    }

    public class Repository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Project Project { get; set; }
        public string DefaultBranch { get; set; }
        public string RemoteUrl { get; set; }
    }

    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public string Visibility { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class PushedBy
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string UniqueName { get; set; }
    }

    public class ResourceContainers
    {
        public Container Collection { get; set; }
        public Container Account { get; set; }
        public Container Project { get; set; }
    }

    public class Container
    {
        public string Id { get; set; }
    }
}