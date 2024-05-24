namespace ArtGallery.Models {
	public class PartialArtwork(int id, string title, string slug, string image, string artist) {
		public int ArtworkId { get; set; } = id;
		public string Title { get; set; } = title;
		public string Slug { get; set; } = slug;
		public string ImageURL { get; set; } = image;
		public string Artist { get; set; } = artist;
	}
}
