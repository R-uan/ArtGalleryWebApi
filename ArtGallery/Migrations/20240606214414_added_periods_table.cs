using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArtGallery.Migrations
{
    /// <inheritdoc />
    public partial class added_periods_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Artworks");

            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "Artworks",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Artists",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    PeriodId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    End = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.PeriodId);
                });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "PeriodId", "End", "Name", "Start", "Summary" },
                values: new object[,]
                {
                    { 1, -3000, "Prehistoric Art", -40000, "Art from the prehistoric era, characterized by cave paintings, carvings, and megalithic structures." },
                    { 2, -332, "Ancient Egyptian Art", -3000, "Art from ancient Egypt, known for its monumental structures, hieroglyphics, and stylized depictions of figures." },
                    { 3, -146, "Ancient Greek Art", -800, "Art from ancient Greece, renowned for its sculptures, pottery, and architectural achievements like the Parthenon." },
                    { 4, 476, "Ancient Roman Art", -146, "Art from ancient Rome, noted for its engineering, architecture, and realistic sculptures." },
                    { 5, 1453, "Byzantine Art", 330, "Art from the Byzantine Empire, characterized by religious mosaics, icons, and architecture." },
                    { 6, 1400, "Medieval Art", 500, "Art from the Middle Ages, including Romanesque and Gothic art, notable for its religious themes and manuscript illuminations." },
                    { 7, 1600, "Renaissance Art", 1400, "Art from the Renaissance period, marked by a revival of classical learning and wisdom, and an emphasis on humanism." },
                    { 8, 1750, "Baroque Art", 1600, "Art from the Baroque period, known for its exuberance, grandeur, and dramatic use of light and shadow." },
                    { 9, 1770, "Rococo Art", 1700, "Art from the Rococo period, characterized by its ornate and decorative style, often with themes of love and nature." },
                    { 10, 1830, "Neoclassicism", 1760, "Art from the Neoclassical period, inspired by the classical art and culture of ancient Greece and Rome." },
                    { 11, 1850, "Romanticism", 1800, "Art from the Romantic period, emphasizing emotion, individualism, and nature." },
                    { 12, 1900, "Realism", 1848, "Art from the Realist period, focused on depicting everyday subjects and scenes in a naturalistic manner." },
                    { 13, 1890, "Impressionism", 1860, "Art from the Impressionist period, characterized by small, thin brush strokes, open composition, and emphasis on light and its changing qualities." },
                    { 14, 1905, "Post-Impressionism", 1886, "Art from the Post-Impressionist period, which extended Impressionism while rejecting its limitations, emphasizing more abstract qualities." },
                    { 15, 1910, "Fauvism", 1905, "Art from the Fauvist period, known for its bold, vibrant colors and simplified forms." },
                    { 16, 1920, "Expressionism", 1905, "Art from the Expressionist period, aiming to depict the emotional experience rather than physical reality." },
                    { 17, 1922, "Cubism", 1907, "Art from the Cubist period, characterized by fragmented and abstracted forms, and multiple perspectives within a single composition." },
                    { 18, 1944, "Futurism", 1909, "Art from the Futurist period, focused on themes of modernity, technology, and movement." },
                    { 19, 1923, "Dada", 1916, "Art from the Dada movement, known for its avant-garde and anti-art stance, often employing absurdity and satire." },
                    { 20, 1966, "Surrealism", 1924, "Art from the Surrealist period, aimed at expressing the unconscious mind through dream-like and fantastical imagery." },
                    { 21, 1965, "Abstract Expressionism", 1943, "Art from the Abstract Expressionist period, characterized by large-scale, abstract paintings that emphasize spontaneous, automatic, or subconscious creation." },
                    { 22, 1970, "Pop Art", 1950, "Art from the Pop Art movement, which draws inspiration from popular and commercial culture, often using irony and parody." },
                    { 23, 1975, "Minimalism", 1960, "Art from the Minimalist period, known for its simplicity, use of geometric shapes, and reduction to essential forms." },
                    { 24, 2024, "Contemporary Art", 1970, "Art from the Contemporary period, encompassing a diverse range of styles, techniques, and themes from the late 20th century to the present." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_PeriodId",
                table: "Artworks",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Periods_PeriodId",
                table: "Artworks",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Periods_PeriodId",
                table: "Artworks");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Artworks_PeriodId",
                table: "Artworks");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Artworks");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Artworks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Artists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
