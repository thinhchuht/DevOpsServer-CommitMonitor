public interface IWebhookService
{
    Task<bool> SetupWebhooksAsync(string webhookUrl, string pat, string collectionName = "", string projectName = "");
}