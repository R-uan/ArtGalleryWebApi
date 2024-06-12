using System.Text.Json.Serialization;
namespace ArtGallery.Models;

public class Museum {
	public int MuseumId { get; set; }
	public string? City { get; set; }
	public string? State { get; set; }
	public float? Latitude { get; set; }
	public float? Longitude { get; set; }
	public int? Inauguration { get; set; }
	public required string Slug { get; set; }
	public required string Name { get; set; }
	public required string Country { get; set; }

	[JsonIgnore] public ICollection<Artwork>? Artworks { get; set; }
}
