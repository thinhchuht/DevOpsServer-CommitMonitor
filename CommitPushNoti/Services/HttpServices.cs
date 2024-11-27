namespace CommitPushNoti.Services
{
    public class HttpServices : IHttpServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _baseAddress;

        public HttpServices(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddress = configuration["Urls:BaseUrl"];
        }

        public async Task<HttpResponseMessage> SetUpProjectWebHookAsync(string relativeUri, object payload, string pat)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseAddress);
            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            return await client.PostAsync(relativeUri, content);
        }

        public async Task<T> GetAsync<T>(string relativeUri, string pat)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseAddress);

            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            var response = await client.GetAsync(relativeUri);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }


    }
}