namespace CommitPushNoti.Data
{
    public class ProjectCollections
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class ProjectCollectionsResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<ProjectCollections> Value { get; set; }
    }
}
