using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommitPushNoti.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitDetail_PullRequests_PullRequestId",
                table: "CommitDetail");

            migrationBuilder.AlterColumn<int>(
                name: "PullRequestId",
                table: "CommitDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitDetail_PullRequests_PullRequestId",
                table: "CommitDetail",
                column: "PullRequestId",
                principalTable: "PullRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitDetail_PullRequests_PullRequestId",
                table: "CommitDetail");

            migrationBuilder.AlterColumn<int>(
                name: "PullRequestId",
                table: "CommitDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitDetail_PullRequests_PullRequestId",
                table: "CommitDetail",
                column: "PullRequestId",
                principalTable: "PullRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
