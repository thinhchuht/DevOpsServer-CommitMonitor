
namespace CommitPushNoti.Services.Interfaces;
public interface IWebhookService
{
    Task<bool> SetupWebhooksAsync(string webhookUrl, string eventType, string pat, string collectionName = "", string projectName = "");
}