namespace CommitPushNoti.Services.Interfaces;
public interface IWebhookService
{
    Task<ResponseModel> SetupWebhooksAsync(string webhookUrl, string eventType, string pat, string collectionName = "", string projectName = "", bool isSetup = true);
}