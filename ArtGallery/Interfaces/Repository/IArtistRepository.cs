using ArtGallery.Models;

namespace ArtGallery.Interfaces {
	public interface IArtistRepository : IBaseRepository<Artist, UpdateArtist, PartialArtist> { }
}
