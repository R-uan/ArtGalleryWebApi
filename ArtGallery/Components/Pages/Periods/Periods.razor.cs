using ArtGallery.Interfaces.Services;
using ArtGallery.Models;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Pages.Periods
{
    public partial class Periods : ComponentBase
    {
        public List<PartialPeriod>? PeriodList { get; set; }
        [Inject]
        public required IPeriodService _periodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PeriodList = await _periodService.Partial();
        }
    }
}
