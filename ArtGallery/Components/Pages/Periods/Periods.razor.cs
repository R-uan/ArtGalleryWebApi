using ArtGallery.Interfaces.Services;
using ArtGallery.Models;
using ArtGallery.Utils;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Pages.Periods
{
    public partial class Periods : ComponentBase, IDisposable
    {
        [Inject] public required EventService EventService { get; set; }
        [Inject] public required IPeriodService PeriodService { get; set; }
        private bool _subscribed = false;
        public List<PartialPeriod>? PeriodList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // Subscribe to deletion event from SinglePeriod component
            if (!_subscribed)
            {
                EventService.OnPeriodDeleted += this.HandleDeleteEvent;
                this._subscribed = true;
            }
            await this.LoadPeriods();
        }

        private async Task LoadPeriods()
        {
            this.PeriodList = await PeriodService.Partial();
            StateHasChanged();
        }

        private async Task HandleDeleteEvent()
        {
            Console.WriteLine("Subscriber recieved the event");
            await this.LoadPeriods();
        }

        public void Dispose()
        {
            EventService.OnPeriodDeleted -= this.HandleDeleteEvent;
        }
    }
}
