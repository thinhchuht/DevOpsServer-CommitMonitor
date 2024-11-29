namespace CommitPushNoti.Infrastructures
{
    public class DevopsContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<CommitDetail> CommitDetail { get; set; }
        public DbSet<UserProject> UserProject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Email);

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

            // UserProject - Composite Key
            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserEmail, up.ProjectId });
        }
    }
}
