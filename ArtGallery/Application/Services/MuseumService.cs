using ArtGallery.Models;
using ArtGallery.Interfaces;

namespace ArtGallery.Services {
	public class MuseumService(IMuseumRepository repository) : IMuseumService {
		private readonly IMuseumRepository _repository = repository;

		public async Task<List<Museum>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<Museum?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Museum> PostOne(Museum museum) {
			return await _repository.SaveOne(museum);
		}

		public async Task<Museum?> UpdateOne(int id, UpdateMuseum museum) {
			return await _repository.UpdateById(id, museum);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Museum?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}

		public async Task<List<PartialMuseum>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

	}
}