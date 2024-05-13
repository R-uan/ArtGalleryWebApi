namespace ArtGallery.Models {
	public class PartialArtwork(string name, string slug, string image, string artist) {
		public string Name { get; set; } = name;
		public string Slug { get; set; } = slug;
		public string ImageURL { get; set; } = image;
		public string Artist { get; set; } = artist;
	}
}
