namespace CommitPushNoti.Infrastructures.Models
{
    public class Collection
    {
        public Collection(string id, string name, string url)
        {
            Id   = id;
            Name = name;
            Url  = url;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("baseUrl")]
        public string Url { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}
