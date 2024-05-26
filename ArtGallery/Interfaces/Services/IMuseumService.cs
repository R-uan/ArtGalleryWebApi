using ArtGallery.DTO;
using ArtGallery.Models;

namespace ArtGallery.Interfaces {
	public interface IMuseumService : IBaseService<Museum, MuseumDTO, UpdateMuseumDTO, PartialMuseumDTO> { }
}
