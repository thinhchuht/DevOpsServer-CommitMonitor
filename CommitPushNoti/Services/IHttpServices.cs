namespace CommitPushNoti.Services
{
    public interface IHttpServices
    {
        Task<HttpResponseMessage> SetUpProjectWebHookAsync(string relativeUri, object payload, string pat);
        Task<T> GetAsync<T>(string relativeUri, string pat);
    }
}