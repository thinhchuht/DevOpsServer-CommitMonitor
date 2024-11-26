using System.Net.Http.Headers;

public class WebhookService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _organization = "DEMO_Renamed"; 

    public WebhookService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> SetupWebhooksAsync(List<string> projectNames, string webhookUrl, string pat)
    {
        var token = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{pat}"));
        Console.WriteLine(token);
        try
        {
            foreach (var projectName in projectNames)
            {
                var client = _httpClientFactory.CreateClient();

                client.BaseAddress = new Uri("https://dev.azure.com/");

                var payload = new
                {
                    eventType = "push",
                    url = webhookUrl
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

                // Use the full URI when calling API
                var response = await client.PostAsJsonAsync(
                    $"{_organization}/{projectName}/_apis/hooks/subscriptions?api-version=7.1", payload);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            // Log error
            return false;
        }
    }
}
