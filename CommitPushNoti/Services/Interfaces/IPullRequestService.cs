namespace CommitPushNoti.Services.Interfaces
{
    public interface IPullRequestService
    {
        Task<ResponseModel> AddPullRequest(PullRequest pullRequest);
        Task<ResponseModel> UpdatePullRequest(PullRequest pullRequest);
    }
}