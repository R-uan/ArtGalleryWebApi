namespace ArtGallery.Models {
	public class Artwork {
		public int ArtworkId { get; set; }
		public int? Year { get; set; }
		public string Slug { get; set; } = String.Empty;
		public string Title { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;

		public int ArtistId { get; set; }
		public Artist? Artist { get; set; }

		public int MuseumId { get; set; }
		public Museum? Museum { get; set; }
	}
}
