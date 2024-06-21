using ArtGallery.Interfaces.Services;
using ArtGallery.Utils;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Landing
{
    public partial class Landing : ComponentBase
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
