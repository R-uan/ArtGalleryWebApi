using System.ComponentModel.DataAnnotations;

namespace ArtGallery.Data.DataTransferObjects.Period
{
    public class UpdatePeriod
    {
        [Range(-500, 2024)] public int? End { get; set; }
        [Range(-500, 2024)] public int? Start { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string? Name { get; set; }
        [StringLength(50, MinimumLength = 4)]
        public string? Summary { get; set; }
    }
}
