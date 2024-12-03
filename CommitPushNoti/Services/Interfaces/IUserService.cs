namespace CommitPushNoti.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel> AddUser(User newUser);
        Task<List<User>> GetAllUser();
        Task<List<UserReporter>> GetUserReport(List<string> selectedEmails, DateTime startDate, DateTime endDate);
    }
}