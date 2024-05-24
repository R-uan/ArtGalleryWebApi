namespace ArtGallery.Models {
	public struct PartialArtist(string name, string slug, int id) {
		public int ArtistId { get; set; } = id;
		public string Name { get; set; } = name;
		public string Slug { get; set; } = slug;
	}
}