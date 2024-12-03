namespace CommitPushNoti.Infrastructures.DbServices
{
    public interface IUserDbService
    {
        Task<List<User>> GetAllUser();
        Task<List<UserReporter>> GetUserReport(List<string> selectedEmails, DateTime startDate, DateTime endDate);
    }
}
