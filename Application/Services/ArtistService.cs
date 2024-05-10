using ArtGallery.Models;
namespace ArtGallery.Services {
	public class ArtistService : IService<Artist, UpdateArtist, ArtistPartial> {

		private readonly IRepository<Artist, UpdateArtist, ArtistPartial> _repository;
		public ArtistService(IRepository<Artist, UpdateArtist, ArtistPartial> repository) {
			_repository = repository;
		}

		public async Task<List<Artist>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<List<ArtistPartial>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

		public async Task<Artist?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artist> PostOne(Artist artist) {
			return await _repository.SaveOne(artist);
		}

		public async Task<bool?> UpdateOne(int id, UpdateArtist artist) {
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