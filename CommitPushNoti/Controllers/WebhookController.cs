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

        [HttpPost("get-noti")]
        public async Task<IActionResult> ReceiveNotification([FromBody] CommitNotification notification)
        {
            if (notification == null)
            {
                return BadRequest("Invalid notification payload.");
            }
            await _notificationService.AddNotificationAsync(notification);
            return Ok(new { message = "Notification received successfully." });
        }

        [HttpGet("notifications")]
        public IActionResult GetNotifications(int page = 1, int pageSize = 3)
        {
            var notifications = _notificationService.GetNotifications(page, pageSize);
            return Ok(notifications);
        }

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
