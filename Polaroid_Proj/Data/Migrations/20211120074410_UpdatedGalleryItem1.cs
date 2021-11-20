using Microsoft.EntityFrameworkCore.Migrations;

namespace Polaroid_Proj.Data.Migrations
{
    public partial class UpdatedGalleryItem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GalleryItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GalleryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
