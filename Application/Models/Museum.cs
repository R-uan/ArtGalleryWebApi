using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models {
	public class Museum {
		public int MuseumId { get; set; }
		[Required] public required string Slug { get; set; }
		[Required] public required string Name { get; set; }
		[Required] public required string Country { get; set; }

		public string? City { get; set; }
		public string? State { get; set; }

		public int? Latitude { get; set; }
		public int? Longitude { get; set; }
		public int? Inauguration { get; set; }

		public ICollection<Artwork>? Artworks { get; set; }
	}

}