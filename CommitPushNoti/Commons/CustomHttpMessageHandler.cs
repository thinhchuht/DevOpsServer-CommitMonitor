using System.Net;

namespace CommitPushNoti.Commons
{
    public class CustomHttpMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized) // Kiểm tra 401
            {
                Console.WriteLine("Unauthorized");
                throw new UnauthorizedAccessException("Authentication failed. Please check your credentials.");
            }

            return response;
        }
    }
}
