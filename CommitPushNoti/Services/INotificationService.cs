namespace CommitPushNoti.Services
{
    public interface INotificationService
    {
        IEnumerable<CommitNotification> GetNotifications(int pageNumber, int pageSize);
        Task AddNotificationAsync(CommitNotification notification);
        List<CommitNotification> GetNotificationsPaged(int page, int pageSize);
        int GetTotalNotificationsCount();
    }
}