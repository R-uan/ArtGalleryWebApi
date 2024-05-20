using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.Utils;

namespace ArtGallery.Services {
	public class ArtworkService(IArtworkRepository repository) : IArtworkService {
		private readonly IArtworkRepository _repository = repository;

		public async Task<List<Artwork>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<List<PartialArtwork>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}
		
		public async Task<PaginatedResponse<Artwork>> GetAllPartialPaginated(int page_index, int page_size) {
			return await _repository.FindAllPartialPaginated(page_index, page_size);
		}

		public async Task<Artwork?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artwork> PostOne(Artwork artist) {
			return await _repository.SaveOne(artist);
		}

		public async Task<Artwork?> UpdateOne(int id, UpdateArtwork artist) {
			return await _repository.UpdateById(id, artist);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Artwork?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}
	}
}
