namespace CommitPushNoti.Services.Classes
{
    public class RepositoryService(IBaseDbServices baseDbServices) : IRepositoryService
    {
        public async Task<ResponseModel> AddRepository(Repository newRepository, string projectId)
        {
            var repository = await baseDbServices.GetByIdAsync<Repository>(newRepository.Id);
            if (repository == null)
            {
                newRepository.ProjectId = projectId;
                return await baseDbServices.AddAsync(newRepository);
            }
            return ResponseModel.GetSuccessResponse("Already exist project");
        }

    }
}
