namespace CommitPushNoti.Services
{
    public class NotificationService : INotificationService
    {
        private readonly List<CommitNotification> _notifications = new();
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IEnumerable<CommitNotification> GetNotifications(int pageNumber, int pageSize)
        {
            return _notifications.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public async Task AddNotificationAsync(CommitNotification notification)
        {
            _notifications.Insert(0, notification);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public List<CommitNotification> GetNotificationsPaged(int page, int pageSize)
        {
            return _notifications
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalNotificationsCount()
        {
            return _notifications.Count;
        }
    }
}