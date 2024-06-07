using ArtGallery.DTO;
using ArtGallery.Models;
namespace ArtGallery.Interfaces;

public interface IArtistRepository : IBaseRepository<Artist, UpdateArtistDTO, PartialArtistDTO, ArtistQueryParams> {
}