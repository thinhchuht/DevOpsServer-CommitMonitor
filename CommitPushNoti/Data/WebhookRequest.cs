namespace CommitPushNoti.Data
{
    public class WebhookRequest
    {
        public string Organization { get; set; }
        public string PAT { get; set; }
        public List<string> ProjectNames { get; set; }
        public string WebhookUrl { get; set; }
    }

    public class WebhookResult
    {
        public string Project { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ProjectsResponse
    {
        public List<Project> Value { get; set; }
    }

}