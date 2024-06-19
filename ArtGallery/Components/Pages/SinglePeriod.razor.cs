using ArtGallery.Models;
using Microsoft.AspNetCore.Components;

namespace ArtGallery.Components.Pages
{
    public partial class SinglePeriod : ComponentBase
    {
        [Parameter]
        public required PartialPeriod Period { get; set; }
    }
}
