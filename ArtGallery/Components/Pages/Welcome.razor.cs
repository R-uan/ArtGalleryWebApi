using ArtGallery.Utils;
using ArtGallery.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Pages
{
    public partial class Welcome : ComponentBase
    {
        private DataSummary? Summary { get; set; }

        [Inject]
        public required IDashboardService Service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Summary = await Service.GetSummary();
        }

    }
}
