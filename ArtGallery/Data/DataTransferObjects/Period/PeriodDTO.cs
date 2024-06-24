using System.ComponentModel.DataAnnotations;
namespace ArtGallery.Models;

public class PeriodDTO
{
    [Required(ErrorMessage = "Please provide a ending year for the period.")]
    public int End { get; set; }
    [Required(ErrorMessage = "Please provide a starting year for the period.")]
    public int Start { get; set; }
    [Required(ErrorMessage = "Please provide a name for the period.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please provide a summary for the period.")]
    public string Summary { get; set; }
}
