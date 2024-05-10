using ArtGallery.Models;

namespace ArtGallery.Services {
	public class MuseumService : IService<Museum, UpdateMuseum, MuseumPartial> {

		private readonly IRepository<Museum, UpdateMuseum, MuseumPartial> _repository;
		public MuseumService(IRepository<Museum, UpdateMuseum, MuseumPartial> repository) {
			_repository = repository;
		}

		public async Task<List<Museum>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<Museum?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Museum> PostOne(Museum museum) {
			return await _repository.SaveOne(museum);
		}

		public async Task<bool?> UpdateOne(int id, UpdateMuseum museum) {
			return await _repository.UpdateById(id, museum);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Museum?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}

		public async Task<List<MuseumPartial>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

	}
}