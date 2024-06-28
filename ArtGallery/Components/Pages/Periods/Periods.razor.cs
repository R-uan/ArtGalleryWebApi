using ArtGallery.Data.DataTransferObjects.Period;
using ArtGallery.Interfaces.Services;
using ArtGallery.Models;
using ArtGallery.Utils;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Pages.Periods
{
    public partial class Periods : ComponentBase, IDisposable
    {
        [Inject] public required IPeriodService PeriodService { get; set; }
        [Inject] public required EventService EventService { get; set; }
        public List<PartialPeriod>? PeriodList { get; set; }
        private bool _subscribed = false;

        public PeriodDTO PeriodModel = new PeriodDTO();
        private async Task HandleCreatePeriodFormSubmit()
        {
            await PeriodService.Save(PeriodModel);
            this.HideCreateModal();
        }
        
        private int PeriodId { get; set; }
        private UpdatePeriod PeriodToUpdate { get; set; } = new UpdatePeriod();

        private async Task HandleUpdatePeriodFormSubmit()
        {
            var update = await PeriodService.Update(this.PeriodId, PeriodToUpdate);
            await this.LoadPeriods();
            this.HideUpdateModal();
        }

        private async Task<bool> FetchPeriodToUpdate(int id)
        {
            var period = await PeriodService.FindById(id);
            if (period != null)
            {
                this.PeriodId = period.PeriodId;
                PeriodToUpdate.End = period.End;
                PeriodToUpdate.Name = period.Name;
                PeriodToUpdate.Start = period.Start;
                PeriodToUpdate.Summary = period.Summary;
                return true;
            }
            return false;
        }

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
