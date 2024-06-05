using System.ComponentModel.DataAnnotations;

namespace ArtGallery.DTO {
	public class MuseumDTO {
		[Required(ErrorMessage = "Slug Required", AllowEmptyStrings = false)]
		public required string Slug { get; set; }
		[Required(ErrorMessage = "Name Required", AllowEmptyStrings = false)]
		public required string Name { get; set; }
		[Required(ErrorMessage = "Country Required", AllowEmptyStrings = false)]
		public required string Country { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public int? Latitude { get; set; }
		public int? Longitude { get; set; }
		public int? Inauguration { get; set; }
	}
}