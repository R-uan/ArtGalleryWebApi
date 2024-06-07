namespace ArtGallery.DTO;

public struct PartialMuseumDTO(int id, string slug, string name, string country) {
	public int MuseumId { get; set; } = id;
	public string Name { get; set; } = name;
	public string Country { get; set; } = country;
	public string Slug { get; set; } = slug;
}
