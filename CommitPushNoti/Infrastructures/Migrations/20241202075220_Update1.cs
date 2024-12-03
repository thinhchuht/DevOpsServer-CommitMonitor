using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitPushNoti.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PullRequestId",
                table: "CommitDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PullRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RepositoryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PullRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PullRequests_Repositories_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PullRequests_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitDetail_PullRequestId",
                table: "CommitDetail",
                column: "PullRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PullRequests_RepositoryId",
                table: "PullRequests",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PullRequests_UserEmail",
                table: "PullRequests",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitDetail_PullRequests_PullRequestId",
                table: "CommitDetail",
                column: "PullRequestId",
                principalTable: "PullRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitDetail_PullRequests_PullRequestId",
                table: "CommitDetail");

            migrationBuilder.DropTable(
                name: "PullRequests");

            migrationBuilder.DropIndex(
                name: "IX_CommitDetail_PullRequestId",
                table: "CommitDetail");

            migrationBuilder.DropColumn(
                name: "PullRequestId",
                table: "CommitDetail");
        }
    }
}
