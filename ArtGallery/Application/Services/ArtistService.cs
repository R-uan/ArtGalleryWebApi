using ArtGallery.Models;
using ArtGallery.Interfaces;

namespace ArtGallery.Services {
	public class ArtistService(IArtistRepository repository) : IArtistService {

		private readonly IArtistRepository _repository = repository;

		public async Task<List<Artist>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<List<PartialArtist>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

		public async Task<Artist?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artist> PostOne(Artist artist) {
			return await _repository.SaveOne(artist);
		}

		public async Task<Artist?> UpdateOne(int id, UpdateArtist artist) {
			return await _repository.UpdateById(id, artist);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Artist?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}
	}
}