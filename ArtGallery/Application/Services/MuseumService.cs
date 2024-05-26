using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;

namespace ArtGallery.Services {
	public class MuseumService(IMuseumRepository repository) : IMuseumService {
		private readonly IMuseumRepository _repository = repository;

		public async Task<List<Museum>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<Museum?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Museum> PostOne(MuseumDTO museum) {
			Museum mapping = new() {
				Country = museum.Country,
				Name = museum.Name,
				Slug = museum.Slug,
				City = museum.City,
				State = museum.State,
				Latitude = museum.Latitude,
				Longitude = museum.Longitude,
				Inauguration = museum.Inauguration,
			};
			return await _repository.SaveOne(mapping);
		}

		public async Task<Museum?> UpdateOne(int id, UpdateMuseumDTO museum) {
			return await _repository.UpdateById(id, museum);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Museum?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}

		public async Task<List<PartialMuseumDTO>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

	}
}