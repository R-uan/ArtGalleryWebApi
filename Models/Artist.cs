using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Models {

	public class Artist {
		public int ArtistId { get; set; }
		public string? Country { get; set; } = String.Empty;
		public string? Movement { get; set; } = String.Empty;
		public string? Description { get; set; } = String.Empty;

		[Required] public string Name { get; set; } = String.Empty;
		[Required] public string Slug { get; set; } = String.Empty;

		public DateTime? Date_of_birth { get; set; }
		public DateTime? Date_of_death { get; set; }

		public ICollection<Artwork> Artworks { get; set; }
	}
}
