using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArtGallery.Models {
	public class Artwork {
		public int ArtworkId { get; set; }
		public required string Slug { get; set; }
		public required string Title { get; set; }
		public required string History { get; set; }
		public required string ImageURL { get; set; }

		public int? Year { get; set; }
		public int? ArtistId { get; set; }
		public int? MuseumId { get; set; }
		public int? PeriodId { get; set; }

		public Period? Period { get; set; }
		public Artist? Artist { get; set; }
		public Museum? Museum { get; set; }
	}
}
