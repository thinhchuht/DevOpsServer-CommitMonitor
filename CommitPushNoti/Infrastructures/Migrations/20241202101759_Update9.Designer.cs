﻿// <auto-generated />
using System;
using CommitPushNoti.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommitPushNoti.Infrastructures.Migrations
{
    [DbContext(typeof(DevopsContext))]
    [Migration("20241202101759_Update9")]
    partial class Update9
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Collection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "baseUrl");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.CommitDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommitMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommitUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineChange")
                        .HasColumnType("int");

                    b.Property<int?>("PullRequestId")
                        .HasColumnType("int");

                    b.Property<string>("RepositoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PullRequestId");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("UserEmail");

                    b.ToTable("CommitDetail");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("CollectionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "lastUpdateTime");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<int>("Revision")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "revision");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "state");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "url");

                    b.Property<string>("Visibility")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "visibility");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.PullRequest", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("UserEmail");

                    b.ToTable("PullRequests");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Repository", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RemoteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.UserProject", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserEmail", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("UserProject");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.CommitDetail", b =>
                {
                    b.HasOne("CommitPushNoti.Infrastructures.Models.PullRequest", "PullRequest")
                        .WithMany("CommitDetails")
                        .HasForeignKey("PullRequestId");

                    b.HasOne("CommitPushNoti.Infrastructures.Models.Repository", "Repository")
                        .WithMany("CommitDetails")
                        .HasForeignKey("RepositoryId");

                    b.HasOne("CommitPushNoti.Infrastructures.Models.User", "User")
                        .WithMany("CommitDetails")
                        .HasForeignKey("UserEmail");

                    b.Navigation("PullRequest");

                    b.Navigation("Repository");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Project", b =>
                {
                    b.HasOne("CommitPushNoti.Infrastructures.Models.Collection", "Collection")
                        .WithMany("Projects")
                        .HasForeignKey("CollectionId");

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.PullRequest", b =>
                {
                    b.HasOne("CommitPushNoti.Infrastructures.Models.Repository", "Repository")
                        .WithMany("PullRequests")
                        .HasForeignKey("RepositoryId");

                    b.HasOne("CommitPushNoti.Infrastructures.Models.User", "User")
                        .WithMany("PullRequests")
                        .HasForeignKey("UserEmail");

                    b.Navigation("Repository");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Repository", b =>
                {
                    b.HasOne("CommitPushNoti.Infrastructures.Models.Project", "Project")
                        .WithMany("Repositories")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.UserProject", b =>
                {
                    b.HasOne("CommitPushNoti.Infrastructures.Models.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommitPushNoti.Infrastructures.Models.User", "User")
                        .WithMany("UserProject")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Collection", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Project", b =>
                {
                    b.Navigation("Repositories");

                    b.Navigation("UserProjects");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.PullRequest", b =>
                {
                    b.Navigation("CommitDetails");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.Repository", b =>
                {
                    b.Navigation("CommitDetails");

                    b.Navigation("PullRequests");
                });

            modelBuilder.Entity("CommitPushNoti.Infrastructures.Models.User", b =>
                {
                    b.Navigation("CommitDetails");

                    b.Navigation("PullRequests");

                    b.Navigation("UserProject");
                });
#pragma warning restore 612, 618
        }
    }
}
