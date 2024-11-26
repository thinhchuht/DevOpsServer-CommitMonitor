using CommitPushNoti.Data;
using CommitPushNoti.Services;
using Microsoft.AspNetCore.Mvc;
using static CommitPushNoti.Pages.WebhookSetup;

namespace CommitPushNoti.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebhookController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WebhookService _webhookService;
        public WebhookController(NotificationService notificationService, IHttpClientFactory httpClientFactory, WebhookService webhookService)
        {
            _notificationService = notificationService;
            _httpClientFactory = httpClientFactory;
            _webhookService = webhookService;
        }

        [HttpPost("get-noti")]
        public async Task<IActionResult> ReceiveNotification([FromBody] CommitNotification notification)
        {
            Console.WriteLine("Start" + DateTime.Now.ToString());
            if (notification == null)
            {
                return BadRequest("Invalid notification payload.");
            }
            await _notificationService.AddNotificationAsync(notification);
            Console.WriteLine("End" + DateTime.Now.ToString());
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
            Console.WriteLine("Setup");
            if (string.IsNullOrWhiteSpace(request.ProjectNames) || string.IsNullOrWhiteSpace(request.WebhookUrl) || string.IsNullOrWhiteSpace(request.PAT))
            {
                return BadRequest("Project names, webhook URL, and PAT are required.");
            }

            var projectNames = request.ProjectNames.Split(',').Select(p => p.Trim()).ToList();
            var result = await _webhookService.SetupWebhooksAsync(projectNames, request.WebhookUrl, request.PAT);

            if (!result)
            {
                return StatusCode(500, "Failed to setup webhooks.");
            }
            return Ok(new { message = "Webhooks setup successfully." });
        }
    }
}
