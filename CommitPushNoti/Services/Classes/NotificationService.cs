namespace CommitPushNoti.Services.Classes
{
    public class NotificationService(IHubContext<NotificationHub> hubContext, IHttpServices httpServices ) : INotificationService
    {
        private readonly List<CommitNotification> _notifications = new();


        public IEnumerable<CommitNotification> GetNotifications(int pageNumber, int pageSize)
        {
            return _notifications.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public async Task TriggerNotificationAsync()
        {
            await hubContext.Clients.All.SendAsync("RefreshCommits");
        }

        public async Task AddNotificationAsync(CommitNotification notification)
        {
            _notifications.Insert(0, notification);
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public int GetTotalNotificationsCount()
        {
            return _notifications.Count;
        }

        public async Task<int> GetLineCount(CommitNotification notification, string pat)
        {
            var projectName = notification.Resource.Repository.Project.Name;
            var repoId = notification.Resource.Repository.Id;
            var commitId = notification.Resource.Commits.FirstOrDefault().CommitId;

            var uri = $"{notification.CollectionName}/{projectName}" +
                $"/_apis/git/repositories/{notification.Resource.Repository.Id}" +
                $"/commits/{commitId}/changes?api-version=6.0";

            var commitPaths = await httpServices.GetCommitPath(uri, pat);

            uri = $"{notification.CollectionName}/{projectName}" +
                $"/_apis/git/repositories/{notification.Resource.Repository.Id}" +
                $"/commits/{commitId}?api-version=6.0";

            var parentCommit = await httpServices.GetParentCommitId(uri, pat);

            // Tạo danh sách các tác vụ
            var tasks = commitPaths.Select(async path =>
            {

                var response = await httpServices.GetLineCount(notification.CollectionName, projectName, repoId, commitId, parentCommit, path, pat);
                return response;
            });

            // Chạy tất cả các tác vụ và cộng tổng kết quả
            var results = await Task.WhenAll(tasks);
            return results.Sum();
        }
    }
}