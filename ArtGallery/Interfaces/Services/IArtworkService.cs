using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IArtworkService : IBaseService<Artwork, UpdateArtwork, PartialArtwork> {
        public Task<PaginatedResponse<Artwork>> GetAllPartialPaginated(int page_index, int page_size);

    }
}
