namespace CommitPushNoti.Data
{
    public class FileChangeResponse
    {
        [JsonPropertyName("dataProviders")]
        public DataProviders DataProviders { get; set; }

        [JsonPropertyName("dataProviderExceptions")]
        public FileDiffDataProvider DataProviderExceptions { get; set; }
    }

    public class DataProviders
    {
        [JsonPropertyName("ms.vss-code-web.file-diff-data-provider")]
        public FileDiffDataProvider FileDiffDataProvider { get; set; }
    }

    public class FileDiffDataProvider
    {
        [JsonPropertyName("blocks")]
        public List<Block> Blocks { get; set; }
    }

    public class Block
    {
        [JsonPropertyName("changeType")]
        public int ChangeType { get; set; }

        [JsonPropertyName("oLine")]
        public int OLine { get; set; }

        [JsonPropertyName("oLinesCount")]
        public int OLinesCount { get; set; }

        [JsonPropertyName("mLine")]
        public int MLine { get; set; }

        [JsonPropertyName("mLinesCount")]
        public int MLinesCount { get; set; }

        [JsonPropertyName("oLines")]
        public List<string> OLines { get; set; }

        [JsonPropertyName("mLines")]
        public List<string> MLines { get; set; }
    }

}
