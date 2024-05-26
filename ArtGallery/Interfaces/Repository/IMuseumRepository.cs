using ArtGallery.DTO;
using ArtGallery.Models;

namespace ArtGallery.Interfaces {
	public interface IMuseumRepository : IBaseRepository<Museum, UpdateMuseumDTO, PartialMuseumDTO> { }
}
