namespace CommitPushNoti.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ResponseModel> AddProject(Project project, string collectionId);
    }
}
