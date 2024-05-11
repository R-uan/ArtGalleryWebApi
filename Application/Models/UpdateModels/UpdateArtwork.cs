namespace ArtGallery.Models {
	public class UpdateArtwork {
		public int? Year { get; set; }

		public string? Slug { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? ImageURL { get; set; }

		public int? ArtistId { get; set; }
		public int? MuseumId { get; set; }
	}
}
