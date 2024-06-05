using ArtGallery.DTO;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IMuseumRepository : IBaseRepository<Museum, UpdateMuseumDTO, PartialMuseumDTO, MuseumQueryParams> {

	}
}
