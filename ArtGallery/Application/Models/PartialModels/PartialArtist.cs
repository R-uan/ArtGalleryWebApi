namespace ArtGallery.Models {
	public struct PartialArtist(string name, string slug) {
		public string Name = name;
		public string Slug = slug;
	}
}