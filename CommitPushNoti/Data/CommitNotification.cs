namespace CommitPushNoti.Data
{
    public class CommitNotification
    {
        public string             SubscriptionId     { get; set; }
        public int                NotificationId     { get; set; }
        public string             Id                 { get; set; }
        public string             EventType          { get; set; }
        public string             PublisherId        { get; set; }
        public Resource           Resource           { get; set; }
        public string             ResourceVersion    { get; set; }
        public ResourceContainers ResourceContainers { get; set; }
        public DateTime           CreatedDate        { get; set; }
        public int                LineCount          { get; set; }

        public string CommitUrl
        {
            get
            {
                if (Resource?.Repository?.Url != null && Resource?.Commits?.FirstOrDefault()?.CommitId != null)
                {
                    // Cắt URL gốc đến phần "http://devops/{projectName}/_apis/projects/{projectId}" và nối thêm thông tin commit
                    var baseUrl = Resource.Repository.Url;
                    var projectPart = baseUrl.Split("/_apis/")[0]; // Cắt URL trước phần _apis

                    // Lấy tên repository từ thông tin repository
                    var repoName = Resource.Repository.Name;
                    var commitId = Resource.Commits?.FirstOrDefault()?.CommitId;
                    var projectName = Resource.Repository.Project.Name; // Tên dự án

                    // Xây dựng URL commit với tên dự án
                    return $"{projectPart}/{projectName}/_git/{repoName}/commit/{commitId}";
                }
                return string.Empty;
            }
        }

        public string CollectionName
        {
            get
            {
                // Lấy RemoteUrl từ repository
                var remoteUrl = Resource?.Repository?.RemoteUrl;

                if (!string.IsNullOrEmpty(remoteUrl))
                {
                    // Tên collection là phần giữa `http://` và tên project/repository
                    var segments = new Uri(remoteUrl).Segments;
                    if (segments.Length > 1)
                    {
                        // Trả về phần segment thứ 2 (tên collection)
                        return segments[1].TrimEnd('/');
                    }
                }
                return "Unknown"; // Nếu không tìm thấy, trả về giá trị mặc định
            }
        }

    }

    public class Resource
    {
        public Repository   Repository { get; set; }
        public List<Commit> Commits    { get; set; }
    }

    //public class Repository
    //{
    //    public string  Id            { get; set; }
    //    public string  Name          { get; set; }
    //    public Project Project       { get; set; }
    //    public string  Url           { get; set; }
    //    public string  RemoteUrl     { get; set; }
    //    public string  DefaultBranch { get; set; }
    //}

    public class Commit
    {
        public string    CommitId  { get; set; }
        public Author    Author    { get; set; }
        public Committer Committer { get; set; }
        public string    Comment   { get; set; }
        public string    Url       { get; set; }
    }

    public class Author
    {
        public string   Name  { get; set; }
        public string   Email { get; set; }
        public DateTime Date  { get; set; }
    }

    public class Committer
    {
        public string   Name  { get; set; }
        public string   Email { get; set; }
        public DateTime Date  { get; set; }
    }

    //public class Project
    //{
    //    public string Id   { get; set; }
    //    public string Name { get; set; }
    //    public string Url  { get; set; }
    //}
    public class ResourceContainers
    {
        public Collection Collection { get; set; }
        public Project    Project { get; set; }
    }
}
