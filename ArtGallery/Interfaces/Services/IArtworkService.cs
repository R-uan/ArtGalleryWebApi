using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces.Services
{
    public interface IArtworkService : IBaseService<Artwork, ArtworkDTO, UpdateArtworkDTO, PartialArtworkDTO>
    {
        Task<PaginatedResponse<PartialArtworkDTO>> PaginatedQuery(ArtworkQueryParams queryParams, int pageIndex);

    }
}