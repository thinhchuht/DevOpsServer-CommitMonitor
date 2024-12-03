namespace CommitPushNoti.Services.Interfaces
{
    public interface IHttpServices
    {
        Task<HttpResponseMessage> SetUpProjectWebHookAsync(string relativeUri, object payload, string pat);
        Task<HttpResponseMessage> DeleteProjectWebHookAsync(string relativeUri, string pat);
        Task<T> GetAsync<T>(string relativeUri, string pat, bool countLines = false);
        Task<List<string>> GetCommitPath(string relativeUri, string pat);
        Task<string> GetParentCommitId(string relativeUri, string pat);
        Task<int> GetLineCount(string collectionName, string projectName, string repoId, string parentCommitId, string commitId, string path, string pat);
    }
}