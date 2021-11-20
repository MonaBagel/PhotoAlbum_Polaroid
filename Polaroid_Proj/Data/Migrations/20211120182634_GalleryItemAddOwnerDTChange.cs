using Microsoft.EntityFrameworkCore.Migrations;

namespace Polaroid_Proj.Data.Migrations
{
    public partial class GalleryItemAddOwnerDTChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GalleryItems");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "GalleryItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "GalleryItems");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GalleryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
