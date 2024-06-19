using ArtGallery.Utils;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Repositories
{
    public class DashboardRepository(GalleryDbContext context) : IDashboardRepository
    {
        private readonly GalleryDbContext _db = context;

        public async Task<DataSummary> DataSummary()
        {
            int artist_count = await _db.Artists.CountAsync();
            int artwork_count = await _db.Artworks.CountAsync();
            int museum_count = await _db.Museums.CountAsync();

            return new DataSummary()
            {
                ArtistCount = artist_count,
                ArtworkCount = artwork_count,
                MuseumCount = museum_count
            };
        }
    }
}
