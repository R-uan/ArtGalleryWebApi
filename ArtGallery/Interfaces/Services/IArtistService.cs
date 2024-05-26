using ArtGallery.DTO;
using ArtGallery.Models;

namespace ArtGallery.Interfaces {
	public interface IArtistService : IBaseService<Artist, ArtistDTO, UpdateArtistDTO, PartialArtistDTO> { }
}
