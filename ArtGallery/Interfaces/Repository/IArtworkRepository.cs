using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IArtworkRepository : IBaseRepository<Artwork, UpdateArtworkDTO, PartialArtworkDTO, ArtworkQueryParams> {
	}
}
