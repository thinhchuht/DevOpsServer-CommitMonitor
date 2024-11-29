namespace CommitPushNoti.Services.Interfaces
{
    public interface IRepositoryService
    {
        Task<ResponseModel> AddRepository(Repository newRepository, string projectId);
    }
}
