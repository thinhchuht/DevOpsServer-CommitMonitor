namespace CommitPushNoti.Services.Classes
{
    public class CommitDetailServices(IBaseDbServices baseDbServices) : ICommitDetailServices
    {
        public async Task<ResponseModel> AddCommitDetail(string id, string commitMessage, DateTime createDate, string commitUrl, int lineChange, string userEmail, string repositoryId)
        {
            var commit = await baseDbServices.GetByIdAsync<CommitDetail>(id);
            if (commit == null) return await baseDbServices.AddAsync(new CommitDetail(id, commitMessage, createDate, commitUrl, lineChange, userEmail, repositoryId));
            return ResponseModel.GetSuccessResponse("Already exist commit");
        }

        public async Task<List<CommitDetail>> GetAllCommitDetails()
        {
            return await baseDbServices.GetAllAsync<CommitDetail>();
        }

        public async Task<(List<CommitDetail> Commits, int TotalCount)> GetPagedCommits(int page, int pageSize)
        {
            var totalCount = await baseDbServices.CountAsync<CommitDetail>();
            var commits = (await GetAllCommitDetails())
                .OrderByDescending(c => c.CreateDate) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            return (commits, totalCount);
        }

    }
}
