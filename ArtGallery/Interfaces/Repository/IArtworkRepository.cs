using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
        public interface IArtworkRepository : IBaseRepository<Artwork, UpdateArtwork, PartialArtwork> {
                public Task<PaginatedResponse<PartialArtwork>> FindAllPartialPaginated(int page_index, int page_size);
        }
}
