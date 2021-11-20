using Microsoft.EntityFrameworkCore.Migrations;

namespace Polaroid_Proj.Data.Migrations
{
    public partial class PhotoChangeAttributeImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "GalleryItems");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "GalleryItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "GalleryItems");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "GalleryItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
