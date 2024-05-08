namespace ArtGallery;

public struct ArtistPartial(string name, string slug) {
	public string Name { get; } = name;
	public string Slug { get; } = slug;
}
