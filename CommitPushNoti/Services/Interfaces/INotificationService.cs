namespace CommitPushNoti.Services.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<CommitNotification> GetNotifications(int pageNumber, int pageSize);
        //Task AddNotificationAsync(CommitNotification notification);
        Task TriggerNotificationAsync();
  
        //List<CommitNotification> GetNotificationsPaged(int page, int pageSize);
        int GetTotalNotificationsCount();
        Task<int> GetLineCount(CommitNotification notification, string pat);
    }
}