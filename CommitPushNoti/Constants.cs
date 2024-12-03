namespace CommitPushNoti
{
    public class Constants
    {
        public const string CollectionsUri = "_apis/projectCollections";

        //Event Type
        public const string PushedCode         = "git.push";
        public const string CreatedPullRequest = "git.pullrequest.created";
        public const string MergedPullRequest  = "git.pullrequest.merged";
    }
}
