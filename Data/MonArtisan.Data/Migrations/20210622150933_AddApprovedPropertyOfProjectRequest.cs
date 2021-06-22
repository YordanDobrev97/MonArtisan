using Microsoft.EntityFrameworkCore.Migrations;

namespace MonArtisan.Data.Migrations
{
    public partial class AddApprovedPropertyOfProjectRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "ProjectRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "ProjectRequests");
        }
    }
}
