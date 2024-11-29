using CommitPushNoti.Infrastructures.Models;

namespace CommitPushNoti.Data
{
    public class ProjectsResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<Project> Value { get; set; }
    }
}
