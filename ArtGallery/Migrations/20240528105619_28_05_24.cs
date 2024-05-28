using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class _28_05_24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_of_birth",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Date_of_death",
                table: "Artists");

            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                table: "Museums",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Museums",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Artists",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Artists");

            migrationBuilder.AlterColumn<int>(
                name: "Longitude",
                table: "Museums",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Latitude",
                table: "Museums",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_of_birth",
                table: "Artists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_of_death",
                table: "Artists",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
