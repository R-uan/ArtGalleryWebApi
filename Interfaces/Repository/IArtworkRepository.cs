using ArtGallery.Models;

namespace ArtGallery.Interfaces {
	public interface IArtworkRepository : IBaseRepository<Artwork, UpdateArtwork, PartialArtwork> { }
}
