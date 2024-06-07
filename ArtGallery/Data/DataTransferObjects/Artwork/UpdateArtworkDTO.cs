namespace ArtGallery.DTO;

public class UpdateArtworkDTO {
	public int? Year { get; set; }
	public int? PeriodId { get; set; }
	public string? Slug { get; set; }
	public string? Title { get; set; }
	public string? History { get; set; }
	public string? ImageURL { get; set; }

	public int? ArtistId { get; set; }
	public int? MuseumId { get; set; }
}

