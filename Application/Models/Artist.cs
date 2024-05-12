using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models {
	public class Artist {
		public int ArtistId { get; set; }
		public string? Country { get; set; }
		public string? Movement { get; set; }
		public string? Description { get; set; }
		public string? Profession { get; set; }

		[Required] public required string Name { get; set; }
		[Required] public required string Slug { get; set; }

		public DateTime? Date_of_birth { get; set; }
		public DateTime? Date_of_death { get; set; }

		public ICollection<Artwork>? Artworks { get; set; }
	}
}
