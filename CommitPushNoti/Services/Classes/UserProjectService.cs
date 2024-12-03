namespace CommitPushNoti.Services.Classes
{
    public class UserProjectService(IBaseDbServices baseDbServices) : IUserProjectService
    {
        public async Task<ResponseModel> AddUserProject(UserProject newUserProject)
        {
            var userProject = await baseDbServices.GetByIdAsync<UserProject>(new object[] { newUserProject.UserEmail, newUserProject.ProjectId });
            if(userProject == null )
            return await baseDbServices.AddAsync(newUserProject);
            return ResponseModel.GetFailureResponse("UserProject is already exist");
        }
    }
}
