using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Services {
	public class ArtistService(IArtistRepository repository) : IArtistService {

		private readonly IArtistRepository _repository = repository;

		public async Task<List<Artist>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<List<PartialArtistDTO>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

		public async Task<Artist?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artist> PostOne(ArtistDTO artist) {
			Artist mapping = new() {
				Name = artist.Name,
				Slug = artist.Slug,
				Country = artist.Country,
				Biography = artist.Biography,
				Movement = artist.Movement,
				Profession = artist.Profession,
				ImageURL = artist.ImageURL
			};
			return await _repository.SaveOne(mapping);
		}

		public async Task<Artist?> UpdateOne(int id, UpdateArtistDTO artist) {
			return await _repository.UpdateById(id, artist);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Artist?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}

		public async Task<PaginatedResponse<PartialArtistDTO>> GetAllPartialPaginated(int page_index, int page_size) {
			return await _repository.FindAllPartialPaginated(page_index, page_size);
		}

		public async Task<PaginatedResponse<PartialArtistDTO>> PaginatedQuery(ArtistQueryParams queryParams, int page) {
			return await _repository.PaginatedQuery(queryParams, page);
		}
	}
}