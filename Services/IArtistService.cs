using ArtGallery.Models;

namespace ArtGallery.Services {
	public interface IArtistService {
		public Task<Artist?> GetOneById(int id);
		public Task<Artist?> GetOneBySlug(string slug);

		public Task<List<Artist>> GetAll();
		public Task<List<ArtistPartial>> GetAllPartial();
		public Task<Artist> PostOne(Artist artist);
		public Task<bool?> UpdateOne(int id, Artist artist);
		public Task<bool?> DeleteOne(int id);
	}
}