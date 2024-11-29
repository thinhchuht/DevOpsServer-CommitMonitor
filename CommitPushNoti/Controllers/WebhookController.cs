namespace CommitPushNoti.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebhookController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IWebhookService _webhookService;
        public WebhookController(INotificationService notificationService, IWebhookService webhookService)
        {
            _notificationService = notificationService;
            _webhookService = webhookService;
        }

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
                var lineCount = await _notificationService.GetLineCount(notification, _storedPAT);
                notification.LineCount = lineCount;
                await _notificationService.AddNotificationAsync(notification);
                return Ok(new { message = "Notification received successfully." });
            }
            catch (Exception ex) 
            {
                return Ok(new { message = $"Exception:{ex.ToString()}." });
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
            var notifications = _notificationService.GetNotifications(page, pageSize);
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
            var result = await _webhookService.SetupWebhooksAsync(request.WebhookUrl, request.PAT, request.CollectionName, request.ProjectName);

            if (!result)
            {
                return StatusCode(500, "Failed to setup webhooks.");
            }
            return Ok(new { message = "Webhooks setup successfully." });
        }
    }
}
