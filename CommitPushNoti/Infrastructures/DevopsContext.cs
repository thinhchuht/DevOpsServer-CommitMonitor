namespace CommitPushNoti.Infrastructures
{
    public class DevopsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<CommitDetail> CommitDetail { get; set; }
        public DbSet<UserProject> UserProject { get; set; }
        public DbSet<PullRequest> PullRequests { get; set; }

        public DevopsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Email);
            modelBuilder.Entity<PullRequest>()
                .Property(pr => pr.Id)
                .ValueGeneratedNever();

            //// AutoInclude for CommitDetails
            //modelBuilder.Entity<CommitDetail>()
            //    .Navigation(cd => cd.Repository).AutoInclude();
            //modelBuilder.Entity<CommitDetail>()
            //    .Navigation(cd => cd.PullRequest).AutoInclude();

            //// AutoInclude for Repository and related entities
            //modelBuilder.Entity<Repository>()
            //    .Navigation(r => r.Project).AutoInclude();
            //modelBuilder.Entity<Repository>()
            //    .Navigation(r => r.PullRequests).AutoInclude();

            //// AutoInclude for PullRequests and related entities
            //modelBuilder.Entity<PullRequest>()
            //    .Navigation(pr => pr.User).AutoInclude();
            //modelBuilder.Entity<PullRequest>()
            //    .Navigation(pr => pr.Repository).AutoInclude();
            //modelBuilder.Entity<PullRequest>()
            //    .Navigation(pr => pr.CommitDetails).AutoInclude();

            //// AutoInclude for Project and related entities
            //modelBuilder.Entity<Project>()
            //    .Navigation(p => p.Collection).AutoInclude();
            //modelBuilder.Entity<Project>()
            //    .Navigation(p => p.UserProjects).AutoInclude();

            //// AutoInclude for Collection
            //modelBuilder.Entity<Collection>()
            //    .Navigation(c => c.Projects).AutoInclude();

            // AutoInclude for User
            //modelBuilder.Entity<User>()
            //    .Navigation(u => u.PullRequests).AutoInclude();
            //modelBuilder.Entity<User>()
            //    .Navigation(u => u.CommitDetails).AutoInclude();


            // User - CommitDetails (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.CommitDetails)
                .WithOne(cd => cd.User)
                .HasForeignKey(cd => cd.UserEmail);

            // User - UserProjects (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserProject)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserEmail);

            // UserProject - Project (Many-to-One)
            modelBuilder.Entity<UserProject>()
                .HasOne(p => p.Project)
                .WithMany(c => c.UserProjects)
                .HasForeignKey(uc => uc.ProjectId);

            // Collection - Projects (One-to-Many)
            modelBuilder.Entity<Collection>()
                .HasMany(c => c.Projects)
                .WithOne(p => p.Collection)
                .HasForeignKey(p => p.CollectionId);

            // Project - Repositories (One-to-Many)
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Repositories)
                .WithOne(r => r.Project)
                .HasForeignKey(r => r.ProjectId);

            // Repository - CommitDetails (One-to-Many)
            modelBuilder.Entity<Repository>()
                .HasMany(r => r.CommitDetails)
                .WithOne(cd => cd.Repository)
                .HasForeignKey(cd => cd.RepositoryId);

            // PullRequest - CommitDetails (One-to-Many)
            modelBuilder.Entity<PullRequest>()
                .HasMany(pr => pr.CommitDetails)
                .WithOne(cd => cd.PullRequest)
                .HasForeignKey(cd => cd.PullRequestId);

            // Cấu hình quan hệ giữa PullRequest và Repository (Many-to-One)
            modelBuilder.Entity<PullRequest>()
                .HasOne(pr => pr.Repository)
                .WithMany(r => r.PullRequests)
                .HasForeignKey(pr => pr.RepositoryId);

            // UserProject - Composite Key
            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserEmail, up.ProjectId });
        }
    }
}
