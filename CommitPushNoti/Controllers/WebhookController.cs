using CommitPushNoti.Data;
using CommitPushNoti.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CommitPushNoti.Controllers
{
    [ApiController]
    [Route("api")]
    public class WebhookController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public WebhookController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("get-noti")]
        public async Task<IActionResult> ReceiveNotification([FromBody] CommitNotification notification)
        {
            Console.WriteLine("Helllo");
            if (notification == null)
            {
                return BadRequest("Invalid notification payload.");
            }

            // Log hoặc xử lý dữ liệu
            Console.WriteLine($"Notification received: {notification.EventType}");
            Console.WriteLine($"Commit message: {notification.Resource.Commits[0].Comment}");

            var message = $"New commit in {notification.Resource.Repository.Name}: {notification.Resource.Commits[0].Comment}";
            Console.WriteLine(message);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            // Ví dụ gửi thông báo real-time qua SignalR hoặc xử lý logic khác
            return Ok(new { message = "Notification received successfully." });
        }
    }
}
