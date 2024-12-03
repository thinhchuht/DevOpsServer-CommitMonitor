namespace CommitPushNoti.Infrastructures.Models
{
    public class UserProject
    {
        public UserProject(string userEmail, string projectId)
        {
            UserEmail = userEmail;
            ProjectId = projectId;
        }

        public string  UserEmail { get; set; }
        public virtual User    User      { get; set; }
        public string  ProjectId { get; set; }
        public virtual Project Project   { get; set; }
    }
}
