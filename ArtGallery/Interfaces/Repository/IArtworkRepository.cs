using ArtGallery.DTO;
using ArtGallery.Models;

namespace ArtGallery.Interfaces.Repository
{
    public interface IArtworkRepository : IBaseRepository<Artwork, UpdateArtworkDTO, PartialArtworkDTO, ArtworkQueryParams>
    {
    }
}

