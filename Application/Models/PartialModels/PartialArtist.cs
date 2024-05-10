namespace ArtGallery.Models {
	public struct ArtistPartial(string name, string slug) {
		public string Name = name;
		public string Slug = slug;
	}
}