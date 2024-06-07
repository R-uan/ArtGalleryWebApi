using System.Text.Json.Serialization;
namespace ArtGallery.Models;

public class Period {
	public int End { get; set; }
	public int Start { get; set; }
	public int PeriodId { get; set; }
	public required string Name { get; set; }
	public required string Summary { get; set; }

	[JsonIgnore] public ICollection<Artwork>? Artworks { get; set; }
}
