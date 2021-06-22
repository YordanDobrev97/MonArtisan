using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonArtisan.Data.Migrations
{
    public partial class ChangePropertiesProjectEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Craftsman",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IsDeleted",
                table: "Projects",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ClientId",
                table: "Projects",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ClientId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ClientId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Craftsman",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
