using ArtGallery.Utils;
using ArtGallery.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using ArtGallery.Models;

namespace ArtGallery.Components.Pages
{
    public partial class Welcome : ComponentBase
    {
        private DataSummary? Summary { get; set; }
        private List<PartialPeriod>? Periods { get; set; }

        [Inject]
        public required IDashboardService Service { get; set; }

        [Inject]
        public required IPeriodService PeriodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Summary = await Service.GetSummary();
            Periods = await PeriodService.Partial();
        }

    }
}
