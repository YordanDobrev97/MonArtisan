using Microsoft.EntityFrameworkCore.Migrations;

namespace MonArtisan.Data.Migrations
{
    public partial class AddReciverAndSenderProjecectRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_UserId",
                table: "ProjectRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectRequests",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRequests_UserId",
                table: "ProjectRequests",
                newName: "IX_ProjectRequests_SenderId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "ProjectRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_ReceiverId",
                table: "ProjectRequests",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_ReceiverId",
                table: "ProjectRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_SenderId",
                table: "ProjectRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_ReceiverId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_SenderId",
                table: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_ReceiverId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "ProjectRequests");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "ProjectRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRequests_SenderId",
                table: "ProjectRequests",
                newName: "IX_ProjectRequests_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_UserId",
                table: "ProjectRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
