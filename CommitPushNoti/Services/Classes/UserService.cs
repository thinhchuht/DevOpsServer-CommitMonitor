namespace CommitPushNoti.Services.Classes
{
    public class UserService(IBaseDbServices baseDbServices, IUserDbService userDbService) : IUserService
    {
        public async Task<ResponseModel> AddUser(User newUser)
        {
            var user = await baseDbServices.GetByIdAsync<User>(newUser.Email);
            if (user == null)
            {
                return await baseDbServices.AddAsync(newUser);
            }
            return ResponseModel.GetSuccessResponse("Already exist User");
        }

        public async Task<List<User>> GetAllUser() => await userDbService.GetAllUser();

        public async Task<List<UserReporter>> GetUserReport(List<string> selectedEmails, DateTime startDate, DateTime endDate)
            => await userDbService.GetUserReport(selectedEmails, startDate, endDate);

    }
}
