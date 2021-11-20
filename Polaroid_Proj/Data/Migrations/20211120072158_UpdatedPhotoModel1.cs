using Microsoft.EntityFrameworkCore.Migrations;

namespace Polaroid_Proj.Data.Migrations
{
    public partial class UpdatedPhotoModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "GalleryItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "GalleryItems");
        }
    }
}
