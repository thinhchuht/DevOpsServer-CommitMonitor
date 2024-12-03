namespace CommitPushNoti.Infrastructures.Models
{
    public class Project
    {
        public Project(string id, string name, string description, string url, string state, int revision, string visibility, DateTime lastUpdateTime, string collectionId)
        {
            Id             = id;
            Name           = name;
            Description    = description;
            Url            = url;
            State          = state;
            Revision       = revision;
            Visibility     = visibility;
            LastUpdateTime = lastUpdateTime;
            CollectionId   = collectionId;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("revision")]
        public int Revision { get; set; }
        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }
        [JsonPropertyName("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
        public string CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
        public virtual List<UserProject> UserProjects { get; set; }
        public virtual List<Repository> Repositories { get; set; }
    }
}
