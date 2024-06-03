using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IArtistRepository : IBaseRepository<Artist, UpdateArtistDTO, PartialArtistDTO> { }
}
