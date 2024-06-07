using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Services {
	public class ArtistService(IArtistRepository repository) : IArtistService {
		private readonly IArtistRepository _repository = repository;

		public async Task<List<Artist>> All() {
			return await _repository.FindAll();
		}

		public async Task<List<PartialArtistDTO>> Partial() {
			return await _repository.FindAllPartial();
		}

		public async Task<Artist?> FindById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artist> Save(ArtistDTO artist) {
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

		public async Task<Artist?> Update(int id, UpdateArtistDTO artist) {
			return await _repository.UpdateById(id, artist);
		}

		public async Task<bool?> Delete(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Artist?> FindBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}

		public async Task<PaginatedResponse<PartialArtistDTO>> PartialPaginated(int pageIndex) {
			return await _repository.FindAllPartialPaginated(pageIndex);
		}

		public async Task<PaginatedResponse<PartialArtistDTO>> PaginatedQuery(ArtistQueryParams queryParams, int page) {
			return await _repository.PaginatedQuery(queryParams, page);
		}
	}
}