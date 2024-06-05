using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IMuseumRepository : IBaseRepository<Museum, UpdateMuseumDTO, PartialMuseumDTO> {
		Task<PaginatedResponse<PartialMuseumDTO>> PaginatedQuery(MuseumQueryParams queryParams, int page);

	}
}
