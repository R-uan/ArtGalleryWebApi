using ArtGallery.Models;

namespace ArtGallery.Interfaces {
	public interface IMuseumRepository : IBaseRepository<Museum, UpdateMuseum, PartialMuseum> { }
}
