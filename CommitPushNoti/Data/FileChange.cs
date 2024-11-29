namespace CommitPushNoti.Data
{
    public class FileChange
    {
        [JsonPropertyName("changes")]
        public List<Change> Changes { get; set; }
    }

    public class Change
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

}
