using ArtGallery.DTO;
using ArtGallery.Interfaces.Services;
using ArtGallery.Utils;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Pages.Artworks
{
    public partial class Artworks : ComponentBase, IDisposable
    {
        [Inject] public required IArtworkService ArtworkService { get; set; }

        public PaginatedResponse<PartialArtworkDTO>? PaginatedResult { get; set; }
        private int _page = 1;


        protected override async Task OnInitializedAsync()
        {
            await LoadArtworks();
        }

        private async Task LoadArtworks()
        {
            this.PaginatedResult = await this.ArtworkService.PartialPaginated(_page);
            StateHasChanged();
        }

        public void Dispose() { }
    }
}
