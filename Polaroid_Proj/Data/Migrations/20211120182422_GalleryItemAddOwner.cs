using Microsoft.EntityFrameworkCore.Migrations;

namespace Polaroid_Proj.Data.Migrations
{
    public partial class GalleryItemAddOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GalleryItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GalleryItems");
        }
    }
}
