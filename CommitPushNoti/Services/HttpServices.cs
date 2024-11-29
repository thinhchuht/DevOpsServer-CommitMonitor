namespace CommitPushNoti.Services
{
    public class HttpServices : IHttpServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseAddress;
        public HttpServices(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddress = configuration["Urls:BaseUrl"];
        }

        /// <summary>
        /// Tạo Webhook cho project với service
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <param name="payload"></param>
        /// <param name="pat"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Phương thức chung cho hàm Get từ api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeUri"></param>
        /// <param name="pat"></param>
        /// <param name="countLines">Tủy chọn cho case đếm số dòng thay đổi</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string relativeUri, string pat, bool countLines = false)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_baseAddress);
                var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

                var response = await client.GetAsync(relativeUri);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                
                //Nếu đếm số dòng
                if (countLines)
                {
                    // Nếu countLines = true, đếm số dòng trong file
                    //json sẽ trả về fil
                    var lineCount = json.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).Length;
                    return (T)Convert.ChangeType(lineCount, typeof(T)); // Trả về số dòng
                }
                return JsonSerializer.Deserialize<T>(json);
            }

            //Trường hợp file json trả exception ( thêm hoặc xóa file) thì trả về  -1 để xử lý tiếp
            catch 
            {
                return (T)Convert.ChangeType(-1, typeof(T));
            }

        }

        public async Task<List<string>> GetCommitPath(string relativeUri, string pat)
        {
            var paths = new List<string>();
            var fileChanged = await GetAsync<FileChange>(relativeUri, pat);
            if (fileChanged?.Changes != null)
            {
                paths.AddRange(fileChanged.Changes.Select(x => x.Item.Path));
            }
            return paths;
        }

        /// <summary>
        /// Lấy Id của commit trước
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <param name="pat"></param>
        /// <returns></returns>
        public async Task<string> GetParentCommitId(string relativeUri, string pat)
        {

            var parentCommit = await GetAsync<ParentCommit>(relativeUri, pat);
            return parentCommit.Parents.FirstOrDefault();
        }


        /// <summary>
        /// Tính số line đã thay đổi
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="projectName"></param>
        /// <param name="repoId"></param>
        /// <param name="parentCommitId"></param>
        /// <param name="commitId"></param>
        /// <param name="path"></param>
        /// <param name="pat"></param>
        /// <returns></returns>
        public async Task<int> GetLineCount(string collectionName, string projectName, string repoId, string parentCommitId, string commitId, string path, string pat)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseAddress);

            var lineCountUri = $"{collectionName}/_apis/Contribution/HierarchyQuery/project/{projectName}?api-version=6.0-preview";

            //thêm token
            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            //body của request
            var payload = new
            {
                contributionIds = new[] { "ms.vss-code-web.file-diff-data-provider" },
                dataProviderContext = new
                {
                    properties = new
                    {
                        repositoryId = repoId,
                        diffParameters = new
                        {
                            includeCharDiffs = true,
                            modifiedPath = path,
                            modifiedVersion = $"GC{commitId}",
                            originalPath = path,
                            originalVersion = $"GC{parentCommitId}",
                            partialDiff = true
                        }
                    }
                }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(lineCountUri, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var fileChangeResponse = JsonSerializer.Deserialize<FileChangeResponse>(responseContent);

            //fileChangeResponse có thể trả về chuỗi json ( có blocks nơi có thông tin tính để tính line) hoặc exception ( khi trong commit xóa 1 file hoặc thêm 1 file mới)
            if (fileChangeResponse.DataProviderExceptions == null) // trường hợp trả về json ( chỉ edit trong file )
            {
                var blocks = fileChangeResponse.DataProviders.FileDiffDataProvider.Blocks;

                // Tính số dòng thay đổi
                var totalModifiedLines = blocks
                    .Where(block => block.ChangeType != 0) // Chỉ lấy các dòng có thay đổi
                    .Sum(block => block.MLinesCount); // Tổng số dòng thay đổi

                return totalModifiedLines;
            }

           
            //trường hợp ra exception ( add hoặc thêm file)
            var commitFileChangeUri = $"{collectionName}/_apis/git/repositories/{repoId}/items?path={path}&api-version=6.0";
            var lineCount = await GetAsync<int>(commitFileChangeUri, pat, true);

            //nếu là case là delete file
            if (lineCount == -1) 
            {
                //lấy ra parentCommit của commit trước
                var parentCommitUri  = $"{collectionName}/{projectName}/_apis/git/repositories/{repoId}/commits/{parentCommitId}?api-version=6.0";
                parentCommitId = await GetParentCommitId(parentCommitUri, pat);

                //gọi lại api đến dòng cho file đã bị xóa với commitId của commit tồn tại file
                var parentCommitFileUri = $"{collectionName}/{projectName}/_apis/git/repositories/{repoId}/items?path={path}&versionDescriptor.version={parentCommitId}&versionDescriptor.versionType=commit&api-version=6.0";
                lineCount = await GetAsync<int>(parentCommitFileUri, pat, true);
            }
            return lineCount;
        }
    }
}