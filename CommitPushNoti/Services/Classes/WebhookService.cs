namespace CommitPushNoti.Services.Classes;
public class WebhookService(IHttpServices httpServices) : IWebhookService
{

    /// <summary>
    /// Tạo webhook cho projects
    /// </summary>
    /// <param name="projectNames"></param>
    /// <param name="webhookUrl">url để devops gọi để gửi thông báo về</param>
    /// <param name="pat"></param>
    /// <returns></returns>
    public async Task<ResponseModel> SetupWebhooksAsync(string webhookUrl, string eventType, string pat, string collectionName = "", string projectName = "", bool isSetup = true)
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
                        if (isSetup) await HandlePostSubcription(subcriptionUri, webhookUrl, eventType, pat, projectItem);
                        else await HandleDeleteSubcription(collectionsItem.Name, pat);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(collectionName))
            {

                var projectUri = $"{collectionName}/_apis/projects?";
                var subcriptionUri = $"{collectionName}/_apis/hooks/subscriptions?api-version=6.0";

                if (string.IsNullOrEmpty(projectName))
                {
                    if (isSetup)
                    {
                        var projects = await httpServices.GetAsync<ProjectsResponse>(projectUri, pat);
                        foreach (var projectItem in projects.Value)
                        {
                            var response = await HandlePostSubcription(subcriptionUri, webhookUrl, eventType, pat, projectItem);
                        }
                    }
                    else await HandleDeleteSubcription(collectionName, pat);

                }
                else
                {
                    if (isSetup)
                    {
                        var projectCounts = await httpServices.GetAsync<ProjectsResponse>(projectUri, pat);
                        var projects = projectCounts.Value.FirstOrDefault(x => x.Name.Equals(projectName));
                        var projectItem = (await httpServices.GetAsync<ProjectsResponse>(projectUri, pat)).Value.FirstOrDefault(x => x.Name.Equals(projectName));
                        var response = await HandlePostSubcription(subcriptionUri, webhookUrl, eventType, pat, projectItem);
                    }
                    else await HandleDeleteSubcription(collectionName, pat, projectName);

                }
            }
            else return ResponseModel.GetFailureResponse("You have to set collection name if the project name is not empty");
 
            return ResponseModel.GetSuccessResponse("Setup hook successed");
        }
        catch (Exception ex)
        {
            return ResponseModel.GetFailureResponse(ex.ToString());
        }
    }

    private async Task<HttpResponseMessage> HandlePostSubcription(string subcriptionUri, string webhookUrl, string eventType, string pat, Project projectItem)
    {
        var payload = new
        {
            publisherId      = "tfs",
            eventType        = eventType,
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
    private async Task<ResponseModel> HandleDeleteSubcription(string collectionName, string pat, string projectName = "")
    {
        try
        {
            string subcriptionUri;
            var getAllSubscriptionsUri = $"{collectionName}/_apis/hooks/subscriptions?api-version=6.0";
            var allSubcriptions = await httpServices.GetAsync<SubscriptionResponse>(getAllSubscriptionsUri, pat);
            if (string.IsNullOrEmpty(projectName))
            {
                foreach (var subscription in allSubcriptions.Value)
                {
                    subcriptionUri = $"{collectionName}/_apis/hooks/subscriptions/{subscription.Id}?api-version=6.0";
                    await httpServices.DeleteProjectWebHookAsync(subcriptionUri, pat);
                }
                return ResponseModel.GetSuccessResponse("Successed");
            }
            var projects = await httpServices.GetAsync<ProjectsResponse>($"{collectionName}/_apis/projects?", pat);
            var project = projects.Value.Where(project => project.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            var subcriptions =  allSubcriptions.Value.Where(s => s.PublisherInputs.ProjectId.Equals(project.Id));
            foreach(var subcription in subcriptions)
            {
                subcriptionUri = $"{collectionName}/_apis/hooks/subscriptions/{subcription.Id}?api-version=6.0";
                await httpServices.DeleteProjectWebHookAsync(subcriptionUri, pat);
            }
            return ResponseModel.GetSuccessResponse("Successed");
        }
        catch (Exception ex) 
        {
            return ResponseModel.GetFailureResponse(ex.ToString());
        }
    }
}
