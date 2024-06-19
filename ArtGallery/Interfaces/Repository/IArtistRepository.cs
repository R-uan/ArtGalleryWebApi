using ArtGallery.DTO;
using ArtGallery.Models;
namespace ArtGallery.Interfaces.Repository
{
    public interface IArtistRepository : IBaseRepository<Artist, UpdateArtistDTO, PartialArtistDTO, ArtistQueryParams>
    {
    }
}