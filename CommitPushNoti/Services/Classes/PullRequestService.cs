
namespace CommitPushNoti.Services.Classes
{
    public class PullRequestService(IBaseDbServices baseDbServices) : IPullRequestService
    {
        public async Task<ResponseModel> AddPullRequest(PullRequest pullRequest)
        {
            var pr = await baseDbServices.GetByIdAsync<PullRequest>(pullRequest.Id);
            if (pr == null)
            {
                return await baseDbServices.AddAsync(pullRequest);
            }
            return ResponseModel.GetFailureResponse("Pull request is already exist");
        }

        public async Task<ResponseModel> UpdatePullRequest(PullRequest pullRequest)
        {
            return await baseDbServices.UpdateAsync(pullRequest);
        }
    }
}
