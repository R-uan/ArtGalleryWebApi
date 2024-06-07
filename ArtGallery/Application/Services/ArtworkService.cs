using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.Utils;
using ArtGallery.DTO;

namespace ArtGallery.Services {
	public class ArtworkService(IArtworkRepository repository) : IArtworkService {
		private readonly IArtworkRepository _repository = repository;

		public async Task<PaginatedResponse<PartialArtworkDTO>> PaginatedQuery(ArtworkQueryParams queryParams, int page) {
			return await _repository.PaginatedQuery(queryParams, page);
		}

		public async Task<List<Artwork>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<List<PartialArtworkDTO>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

		public async Task<PaginatedResponse<PartialArtworkDTO>> GetAllPartialPaginated(int pageIndex) {
			return await _repository.FindAllPartialPaginated(pageIndex);
		}

		public async Task<Artwork?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artwork> PostOne(ArtworkDTO artwork) {
			Artwork mapping = new() {
				Title = artwork.Title,
				ArtistId = artwork.ArtistId,
				History = artwork.History,
				Slug = artwork.Slug,
				ImageURL = artwork.ImageURL,
				PeriodId = artwork.PeriodId,
				MuseumId = artwork.MuseumId,
				Year = artwork.Year
			};
			return await _repository.SaveOne(mapping);
		}

		public async Task<Artwork?> UpdateOne(int id, UpdateArtworkDTO artist) {
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
