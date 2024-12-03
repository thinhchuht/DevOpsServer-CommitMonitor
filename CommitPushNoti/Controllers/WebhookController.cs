namespace CommitPushNoti.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebhookController(INotificationService notificationService,
        IWebhookService webhookService,
        ICollectionService collectionService,
        IProjectService projectService,
        IRepositoryService repositoryService,
        ICommitDetailServices commitDetailServices,
        IUserService userService,
        IUserProjectService userProjectService,
        IPullRequestService pullRequestService) : ControllerBase
    {
        private static string? _storedPAT;

        /// <summary>
        /// API đổi Personal Access Token
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        [HttpPost("save")]
        public IActionResult SavePAT([FromBody] string pat)
        {
            if (string.IsNullOrWhiteSpace(pat))
            {
                return BadRequest("PAT cannot be empty.");
            }

            _storedPAT = pat;
            return Ok("PAT saved successfully.");
        }

        /// <summary>
        /// API để devops có thể call đến khi 1 code được push lên
        /// </summary>
        /// <param name="notification">Tổng hợp các thông tin về commit đó</param>
        /// <returns></returns>
        [HttpPost("get-noti")]
        public async Task<IActionResult> ReceiveNotification([FromBody] CommitNotification notification)
        {
            try
            {
                if (notification == null)
                {
                    return BadRequest("Invalid notification payload.");
                }

                var projectPart = notification.Resource.Repository.Url.Split("/_apis/")[0]; // Cắt URL trước phần _apis
                var repoName = notification.Resource.Repository.Name;
                var projectName = notification.Resource.Repository.Project.Name;


                await collectionService.AddCollection(notification.ResourceContainers.Collection.Id, notification.CollectionName, notification.ResourceContainers.Collection.Url);

                await projectService.AddProject(notification.Resource.Repository.Project, notification.ResourceContainers.Collection.Id);

                await repositoryService.AddRepository(notification.Resource.Repository, notification.ResourceContainers.Project.Id);
                foreach(var commit in notification.Resource.Commits)
                {
                    var email = commit.Committer.Email ?? $"{commit.Committer.Name}@fpts.com.vn";
                    var lineCount = await notificationService.GetLineCount(notification, commit.CommitId, _storedPAT);
                    var commitUrl = $"{projectPart}/{projectName}/_git/{repoName}/commit/{commit.CommitId}";
                    await userService.AddUser(new User(email, commit.Committer.Name));
                    await userProjectService.AddUserProject(new UserProject(email, notification.ResourceContainers.Project.Id));
                    await commitDetailServices.AddCommitDetail(commit.CommitId, commit.Comment, notification.CreatedDate, commitUrl, lineCount, email, notification.Resource.Repository.Id);
                }
                
                await notificationService.TriggerNotificationAsync();
                return Ok(new { message = "Notification received successfully." });
            }
            catch (Exception ex)
            {
                return Ok(new { message = $"Exception:{ex}." });
            }
        }

        /// <summary>
        /// API để devops có thể call đến khi 1 code được push lên
        /// </summary>
        /// <param name="notification">Tổng hợp các thông tin về commit đó</param>
        /// <returns></returns>
        [HttpPost("get-noti-pr")]
        public async Task<IActionResult> ReceivePR([FromBody] PullRequestNotification notification)
        {
            try
            {
                if (notification == null)
                {
                    return BadRequest("Invalid notification payload.");
                }
                var newPr = new PullRequest(notification.Resource.PullRequestId, notification.Resource.Status,
                    notification.CreatedDate, notification.Resource.Title, notification.Resource.Description, 
                    notification.Resource.Url, notification.Resource.CreatedBy.Email, notification.Resource.Repository.Id);
                if (notification.EventType == Constants.CreatedPullRequest)
                {
                    await pullRequestService.AddPullRequest(newPr);
                    foreach (var commit in notification.Resource.Commits) 
                    {
                        await commitDetailServices.UpdateCommitDetail(commit.CommitId, newPr.Id);
                    }
                }
                if (notification.EventType == Constants.MergedPullRequest) await pullRequestService.UpdatePullRequest(newPr);
                await notificationService.TriggerNotificationAsync();
                return Ok(new { message = "Notification received successfully." });
            }
            catch (Exception ex)
            {
                return Ok(new { message = $"Exception:{ex}." });
            }
        }

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("notifications")]
        public IActionResult GetNotifications(int page = 1, int pageSize = 3)
        {
            var notifications = notificationService.GetNotifications(page, pageSize);
            return Ok(notifications);
        }


        /// <summary>
        /// API để tạo webhooks giữa devops server và service
        /// </summary>
        /// <param name="request">Nếu projectName và collectionName null thì sẽ set cho tất cả các project</param>
        /// <param name="request">Nếu webhook url sẽ url lấy từ action ReceiveNotification</param>
        /// <returns></returns>
        [HttpPost("setup")]
        public async Task<IActionResult> SetupWebhook([FromBody] WebhookSetupRequest request)
        {
            var result = await webhookService.SetupWebhooksAsync(request.WebhookUrl, request.EventType, request.PAT, request.CollectionName, request.ProjectName);

            if (!result.IsSuccess()) return StatusCode(500, result.Message);
            return Ok(new { message = result.Message });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteWebhook([FromBody] WebhookSetupRequest request)
        {
            var result = await webhookService.SetupWebhooksAsync(request.WebhookUrl, request.EventType, request.PAT, request.CollectionName, request.ProjectName, false);

            if (!result.IsSuccess()) return StatusCode(500, result.Message);
            return Ok(new { message = result.Message });
        }
    }
}
