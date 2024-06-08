using System.ComponentModel.DataAnnotations;
namespace ArtGallery.Models;

public class PeriodDTO {
	public int End { get; set; }
	public int Start { get; set; }
	public required string Name { get; set; }
	public required string Summary { get; set; }
}
