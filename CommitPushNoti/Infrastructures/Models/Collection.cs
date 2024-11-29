namespace CommitPushNoti.Infrastructures.Models
{
    public class Collection
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
        public List<Project> Projects { get; set; }
        public List<UserCollection> UserCollection { get; set; }
    }
}
