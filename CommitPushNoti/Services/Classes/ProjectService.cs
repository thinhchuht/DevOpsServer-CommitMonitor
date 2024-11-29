namespace CommitPushNoti.Services.Classes
{
    public class ProjectService(IBaseDbServices baseDbServices) : IProjectService
    {
        public async Task<ResponseModel> AddProject(Project newProject, string collectionId)
        {
            var project = await baseDbServices.GetByIdAsync<Project>(newProject.Id);
            if (project == null)
            {
                newProject.CollectionId = collectionId;
                return await baseDbServices.AddAsync(newProject);
            }
            return ResponseModel.GetSuccessResponse("Already exist project");
        }
    }
}
