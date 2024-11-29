namespace CommitPushNoti.Data
{
    public class ParentCommit
    {
        [JsonPropertyName("parents")]
        public List<string> Parents { get; set; }
    }
}
