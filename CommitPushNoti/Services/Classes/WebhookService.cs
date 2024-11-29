public class WebhookService(IHttpServices httpServices) : IWebhookService
{

    /// <summary>
    /// Tạo webhook cho projects
    /// </summary>
    /// <param name="projectNames"></param>
    /// <param name="webhookUrl">url để devops gọi để gửi thông báo về</param>
    /// <param name="pat"></param>
    /// <returns></returns>
    public async Task<bool> SetupWebhooksAsync(string webhookUrl, string pat, string collectionName = "", string projectName = "")
    {
        try
        {
            if (string.IsNullOrEmpty(collectionName) && string.IsNullOrEmpty(projectName))
            {
                var collections = await httpServices.GetAsync<CollectionsResponse>(Constants.CollectionsUri, pat);
                foreach (var collectionsItem in collections.Value)
                {
                    var projectUri = $"{collectionsItem.Name}/_apis/projects?";
                    var projects = await httpServices.GetAsync<ProjectsResponse>(projectUri, pat);
                    foreach (var projectItem in projects.Value)
                    {
                        var subcriptionUri = $"{collectionsItem.Name}/_apis/hooks/subscriptions?api-version=6.0";
                        var response = await HandlePostSubcription(subcriptionUri,webhookUrl, pat, projectItem);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(collectionName))
            {
                var projectUri = $"{collectionName}/_apis/projects?";
                var subcriptionUri = $"{collectionName}/_apis/hooks/subscriptions?api-version=6.0";

                if (string.IsNullOrEmpty(projectName))
                {
                    var projects = await httpServices.GetAsync<ProjectsResponse>(projectUri, pat);
                    foreach (var projectItem in projects.Value)
                    {
                        var response = await HandlePostSubcription(subcriptionUri, webhookUrl, pat, projectItem);
                    }
                } 
                else
                {
                    var projectCounts = await httpServices.GetAsync<ProjectsResponse>(projectUri, pat);
                    var projects = projectCounts.Value.FirstOrDefault(x => x.Name.Equals(projectName));
                    var projectItem = (await httpServices.GetAsync<ProjectsResponse>(projectUri, pat)).Value.FirstOrDefault(x => x.Name.Equals(projectName));
                    var response = await HandlePostSubcription(subcriptionUri, webhookUrl, pat, projectItem);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private async Task<HttpResponseMessage> HandlePostSubcription(string subcriptionUri, string webhookUrl, string pat, Project projectItem)
    {
        var payload = new
        {
            publisherId      = "tfs",
            eventType        = "git.push",
            resourceVersion  = "1.0",
            consumerId       = "webHooks",
            consumerActionId = "httpRequest",
            consumerInputs   = new
            {
                url = webhookUrl
            },
            publisherInputs = new
            {
                projectId = projectItem.Id,
            },
            status = "enabled"
        };
       return await httpServices.SetUpProjectWebHookAsync(subcriptionUri, payload, pat);
    }
}
