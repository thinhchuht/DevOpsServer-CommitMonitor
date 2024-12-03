namespace CommitPushNoti.Data
{
    public class SubscriptionResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<SubscriptionValue> Value { get; set; }
    }

    public class SubscriptionValue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("publisherInputs")]
        public PublisherInputs PublisherInputs { get; set; }
    }

    public class PublisherInputs
    {
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; }
    }
}
