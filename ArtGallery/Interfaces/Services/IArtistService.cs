using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IArtistService : IBaseService<Artist, ArtistDTO, UpdateArtistDTO, PartialArtistDTO> {
        public Task<PaginatedResponse<PartialArtistDTO>> GetAllPartialPaginated(int page_index, int page_size);
    }
}
