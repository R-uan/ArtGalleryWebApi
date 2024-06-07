using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery;

public class PeriodSeeder(ModelBuilder modelBuilder) {
	private readonly ModelBuilder _modelBuilder = modelBuilder;

	public void Seed() {
		var artPeriods = new List<Period>() {
		new() { PeriodId = 1, Name = "Prehistoric Art", Summary = "Art from the prehistoric era, characterized by cave paintings, carvings, and megalithic structures.", Start = -40000, End = -3000 },
		new() { PeriodId = 2, Name = "Ancient Egyptian Art", Summary = "Art from ancient Egypt, known for its monumental structures, hieroglyphics, and stylized depictions of figures.", Start = -3000, End = -332 },
		new() { PeriodId = 3, Name = "Ancient Greek Art", Summary = "Art from ancient Greece, renowned for its sculptures, pottery, and architectural achievements like the Parthenon.", Start = -800, End = -146 },
		new() { PeriodId = 4, Name = "Ancient Roman Art", Summary = "Art from ancient Rome, noted for its engineering, architecture, and realistic sculptures.", Start = -146, End = 476 },
		new() { PeriodId = 5, Name = "Byzantine Art", Summary = "Art from the Byzantine Empire, characterized by religious mosaics, icons, and architecture.", Start = 330, End = 1453 },
		new() { PeriodId = 6, Name = "Medieval Art", Summary = "Art from the Middle Ages, including Romanesque and Gothic art, notable for its religious themes and manuscript illuminations.", Start = 500, End = 1400 },
		new() { PeriodId = 7, Name = "Renaissance Art", Summary = "Art from the Renaissance period, marked by a revival of classical learning and wisdom, and an emphasis on humanism.", Start = 1400, End = 1600 },
		new() { PeriodId = 8, Name = "Baroque Art", Summary = "Art from the Baroque period, known for its exuberance, grandeur, and dramatic use of light and shadow.", Start = 1600, End = 1750 },
		new() { PeriodId = 9, Name = "Rococo Art", Summary = "Art from the Rococo period, characterized by its ornate and decorative style, often with themes of love and nature.", Start = 1700, End = 1770 },
		new() { PeriodId = 10, Name = "Neoclassicism", Summary = "Art from the Neoclassical period, inspired by the classical art and culture of ancient Greece and Rome.", Start = 1760, End = 1830 },
		new() { PeriodId = 11, Name = "Romanticism", Summary = "Art from the Romantic period, emphasizing emotion, individualism, and nature.", Start = 1800, End = 1850 },
		new() { PeriodId = 12, Name = "Realism", Summary = "Art from the Realist period, focused on depicting everyday subjects and scenes in a naturalistic manner.", Start = 1848, End = 1900 },
		new() { PeriodId = 13, Name = "Impressionism", Summary = "Art from the Impressionist period, characterized by small, thin brush strokes, open composition, and emphasis on light and its changing qualities.", Start = 1860, End = 1890 },
		new() { PeriodId = 14, Name = "Post-Impressionism", Summary = "Art from the Post-Impressionist period, which extended Impressionism while rejecting its limitations, emphasizing more abstract qualities.", Start = 1886, End = 1905 },
		new() { PeriodId = 15, Name = "Fauvism", Summary = "Art from the Fauvist period, known for its bold, vibrant colors and simplified forms.", Start = 1905, End = 1910 },
		new() { PeriodId = 16, Name = "Expressionism", Summary = "Art from the Expressionist period, aiming to depict the emotional experience rather than physical reality.", Start = 1905, End = 1920 },
		new() { PeriodId = 17, Name = "Cubism", Summary = "Art from the Cubist period, characterized by fragmented and abstracted forms, and multiple perspectives within a single composition.", Start = 1907, End = 1922 },
		new() { PeriodId = 18, Name = "Futurism", Summary = "Art from the Futurist period, focused on themes of modernity, technology, and movement.", Start = 1909, End = 1944 },
		new() { PeriodId = 19, Name = "Dada", Summary = "Art from the Dada movement, known for its avant-garde and anti-art stance, often employing absurdity and satire.", Start = 1916, End = 1923 },
		new() { PeriodId = 20, Name = "Surrealism", Summary = "Art from the Surrealist period, aimed at expressing the unconscious mind through dream-like and fantastical imagery.", Start = 1924, End = 1966 },
		new() { PeriodId = 21, Name = "Abstract Expressionism", Summary = "Art from the Abstract Expressionist period, characterized by large-scale, abstract paintings that emphasize spontaneous, automatic, or subconscious creation.", Start = 1943, End = 1965 },
		new() { PeriodId = 22, Name = "Pop Art", Summary = "Art from the Pop Art movement, which draws inspiration from popular and commercial culture, often using irony and parody.", Start = 1950, End = 1970 },
		new() { PeriodId = 23, Name = "Minimalism", Summary = "Art from the Minimalist period, known for its simplicity, use of geometric shapes, and reduction to essential forms.", Start = 1960, End = 1975 },
		new() { PeriodId = 24, Name = "Contemporary Art", Summary = "Art from the Contemporary period, encompassing a diverse range of styles, techniques, and themes from the late 20th century to the present.", Start = 1970, End = 2024 }
	};
		_modelBuilder.Entity<Period>(period => { period.HasData(artPeriods); });
	}
}
