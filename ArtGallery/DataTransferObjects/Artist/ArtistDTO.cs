using System.ComponentModel.DataAnnotations;

namespace ArtGallery.DTO {
	public class ArtistDTO {
		[Required(ErrorMessage = "Name required", AllowEmptyStrings = false)]
		public required string Name { get; set; }
		[Required(ErrorMessage = "Slug required", AllowEmptyStrings = false)]
		public required string Slug { get; set; }
		[Required(ErrorMessage = "Country required", AllowEmptyStrings = false)]
		public required string Country { get; set; }

		public string? Movement { get; set; }
		public string? Biography { get; set; }
		public string? Profession { get; set; }

		public DateTime? Date_of_birth { get; set; }
		public DateTime? Date_of_death { get; set; }

	}
}
