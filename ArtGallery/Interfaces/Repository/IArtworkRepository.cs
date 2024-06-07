using ArtGallery.DTO;
using ArtGallery.Models;

namespace ArtGallery.Interfaces;
public interface IArtworkRepository : IBaseRepository<Artwork, UpdateArtworkDTO, PartialArtworkDTO, ArtworkQueryParams> {
}

