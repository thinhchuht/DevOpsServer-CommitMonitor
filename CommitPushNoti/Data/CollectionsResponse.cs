using CommitPushNoti.Infrastructures.Models;

namespace CommitPushNoti.Data
{
    public class CollectionsResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<Collection> Value { get; set; }
    }
}
