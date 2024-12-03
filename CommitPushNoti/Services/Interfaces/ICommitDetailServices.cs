namespace CommitPushNoti.Services.Interfaces
{
    public interface ICommitDetailServices
    {
        Task<ResponseModel> AddCommitDetail(string id, string commitMessage, DateTime createDate, string commitUrl, int lineChange, string userEmail, string repositoryId);
        Task<List<CommitDetail>> GetAllCommitDetails();
        Task<(List<CommitDetail> Commits, int TotalCount)> GetPagedCommits(int page, int pageSize);
        Task<ResponseModel> UpdateCommitDetail(string commitId, int prId);
    }
}
