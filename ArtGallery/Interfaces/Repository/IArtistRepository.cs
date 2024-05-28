using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IArtistRepository : IBaseRepository<Artist, UpdateArtistDTO, PartialArtistDTO> {
        public Task<PaginatedResponse<PartialArtistDTO>> FindAllPartialPaginated(int page_index, int page_size);
    }
}
