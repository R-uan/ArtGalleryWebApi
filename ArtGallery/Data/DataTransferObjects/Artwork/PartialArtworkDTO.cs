namespace ArtGallery.DTO;

public class PartialArtworkDTO {
	public int? ArtworkId { get; set; }
	public string? Title { get; set; }
	public string? Slug { get; set; }
	public string? ImageURL { get; set; }
	public string? Artist { get; set; }

	public PartialArtworkDTO() { }

	public PartialArtworkDTO(int id, string title, string slug, string image, string artist) {
		ArtworkId = id;
		Title = title;
		Slug = slug;
		ImageURL = image;
		Artist = artist;
	}
}

