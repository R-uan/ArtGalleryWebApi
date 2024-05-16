using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class _16_06_2024_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Museums",
                newName: "MuseumId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artworks",
                newName: "ArtworkId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artists",
                newName: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MuseumId",
                table: "Museums",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ArtworkId",
                table: "Artworks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Artists",
                newName: "Id");
        }
    }
}
