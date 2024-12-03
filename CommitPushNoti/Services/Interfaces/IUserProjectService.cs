namespace CommitPushNoti.Services.Interfaces
{
    public interface IUserProjectService
    {
        Task<ResponseModel> AddUserProject(UserProject newUserProject);
    }
}