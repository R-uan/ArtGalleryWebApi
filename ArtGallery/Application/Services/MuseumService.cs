using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Services {
	public class MuseumService(IMuseumRepository repository) : IMuseumService {
		private readonly IMuseumRepository _repository = repository;

		public async Task<List<Museum>> All() {
			return await _repository.FindAll();
		}

		public async Task<Museum?> FindById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Museum> Save(MuseumDTO museum) {
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

		public async Task<Museum?> Update(int id, UpdateMuseumDTO museum) {
			return await _repository.UpdateById(id, museum);
		}

		public async Task<bool?> Delete(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Museum?> FindBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}

		public async Task<List<PartialMuseumDTO>> Partial() {
			return await _repository.FindAllPartial();
		}

		public async Task<PaginatedResponse<PartialMuseumDTO>> PartialPaginated(int pageIndex) {
			return await _repository.FindAllPartialPaginated(pageIndex);
		}

		public async Task<PaginatedResponse<PartialMuseumDTO>> PaginatedQuery(MuseumQueryParams queryParams, int page) {
			return await _repository.PaginatedQuery(queryParams, page);
		}
	}
}