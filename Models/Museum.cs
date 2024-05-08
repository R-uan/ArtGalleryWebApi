namespace ArtGallery.Models {

	public class Museum {
		public int MuseumId { get; set; }
		public int Inauguration_year { get; set; }
		public string Slug { get; set; } = String.Empty;
		public string Name { get; set; } = String.Empty;

		public string City { get; set; } = String.Empty;
		public string State { get; set; } = String.Empty;
		public string Country { get; set; } = String.Empty;

		public int Latitude { get; set; } = 0;
		public int Longitude { get; set; } = 0;

		public ICollection<Artwork> Artworks { get; set; }
	}

}