namespace CommitPushNoti.Infrastructures.Models
{
    public class Project
    {
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
        public Collection Collection { get; set; }
        public List<Repository> Repositories { get; set; }
    }
}
