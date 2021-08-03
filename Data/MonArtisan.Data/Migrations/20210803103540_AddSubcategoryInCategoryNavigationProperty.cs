using Microsoft.EntityFrameworkCore.Migrations;

namespace MonArtisan.Data.Migrations
{
    public partial class AddSubcategoryInCategoryNavigationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SubCategories");

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubCategoryId",
                table: "Categories",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SubCategories_SubCategoryId",
                table: "Categories",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SubCategories_SubCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SubCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "SubCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
