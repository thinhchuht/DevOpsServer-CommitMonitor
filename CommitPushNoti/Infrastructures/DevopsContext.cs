using CommitPushNoti.Infrastructures.Models;
using Microsoft.EntityFrameworkCore;

namespace CommitPushNoti.Infrastructures
{
    public class DevopsContext : DbContext
    {
        public DevopsContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<CommitDetail> CommitDetail { get; set; }
        public DbSet<UserCollection> UserCollection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - CommitDetails (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.CommitDetails)
                .WithOne(cd => cd.User)
                .HasForeignKey(cd => cd.UserId);

            // User - UserCollections (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserCollection)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            // UserCollection - Collection (Many-to-One)
            modelBuilder.Entity<UserCollection>()
                .HasOne(uc => uc.Collection)
                .WithMany(c => c.UserCollection)
                .HasForeignKey(uc => uc.CollectionId);

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

            // UserCollection - Composite Key
            modelBuilder.Entity<UserCollection>()
                .HasKey(uc => new { uc.UserId, uc.CollectionId });
        }
    }
}
