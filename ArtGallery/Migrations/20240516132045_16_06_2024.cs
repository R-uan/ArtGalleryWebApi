using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class _16_06_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MuseumId",
                table: "Museums",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Artworks",
                newName: "Period");

            migrationBuilder.RenameColumn(
                name: "ArtworkId",
                table: "Artworks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Artists",
                newName: "Biography");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Artists",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Artworks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "History",
                table: "Artworks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Museums",
                newName: "MuseumId");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "Artworks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artworks",
                newName: "ArtworkId");

            migrationBuilder.RenameColumn(
                name: "Biography",
                table: "Artists",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artists",
                newName: "ArtistId");
        }
    }
}
