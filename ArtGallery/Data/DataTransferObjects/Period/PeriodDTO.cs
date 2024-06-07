using System.ComponentModel.DataAnnotations;
namespace ArtGallery.Models;

public class PeriodDTO {
	public int End { get; set; }
	public int Start { get; set; }
	[Required(ErrorMessage = "Period name required", AllowEmptyStrings = false)]
	public required string Name { get; set; }
	[Required(ErrorMessage = "Provide a summary for the period", AllowEmptyStrings = false)]
	public required string Summary { get; set; }
}
