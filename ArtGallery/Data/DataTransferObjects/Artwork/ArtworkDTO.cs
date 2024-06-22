using System.ComponentModel.DataAnnotations;
namespace ArtGallery.DTO;

public class ArtworkDTO {
	public int? Year { get; set; }
	//[Required(ErrorMessage = "Slug Required", AllowEmptyStrings = false)]
	public required string Slug { get; set; }
	//[Required(ErrorMessage = "Title Required", AllowEmptyStrings = false)]
	public required string Title { get; set; }
	//[Required(ErrorMessage = "History Required", AllowEmptyStrings = false)]
	public required string History { get; set; }
	//[Required(ErrorMessage = "ImageURL Required", AllowEmptyStrings = false)]
	public required string ImageURL { get; set; }
	//[Required(ErrorMessage = "Artist Required", AllowEmptyStrings = false)]
	public required int ArtistId { get; set; }
	//[Required(ErrorMessage = "Museum Required", AllowEmptyStrings = false)]
	public required int MuseumId { get; set; }
	//[Required(ErrorMessage = "Period Required", AllowEmptyStrings = false)]
	public required int PeriodId { get; set; }
}
