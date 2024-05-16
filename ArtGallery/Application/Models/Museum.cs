using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArtGallery.Models {
	public class Museum {
		[Required] public required string Slug { get; set; }
		[Required] public required string Name { get; set; }
		[Required] public required string Country { get; set; }

		public string? City { get; set; }
		public string? State { get; set; }
		public int? Latitude { get; set; }
		public int? Longitude { get; set; }
		public int? Inauguration { get; set; }

		[JsonIgnore] public ICollection<Artwork>? Artworks { get; set; }
		[JsonIgnore] public int MuseumId { get; set; }
	}
}