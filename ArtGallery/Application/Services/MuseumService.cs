using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Services
{
	public class MuseumService(IMuseumRepository repository, IRedisRepository redis) : IMuseumService
	{
		private readonly IMuseumRepository _repository = repository;
		private readonly IRedisRepository _redis = redis;
		//
		//
		//
		public async Task<List<Museum>> All()
		{
			//	Check for cache and return it if available;
			var cache = await _redis.Get<List<Museum>>("all-museum");
			if (cache != null) return cache;

			//	In case of null cache: calls the repository for data retrieval and cache it;
			var find = await _repository.Find();
			await _redis.Store<List<Museum>>("all-museum", find);
			return find;
		}
		//
		//
		//
		public async Task<List<PartialMuseumDTO>> Partial()
		{
			//	Check for cache and return it if available;
			var cache = await _redis.Get<List<PartialMuseumDTO>>("partial-museum");
			if (cache != null) return cache;

			//	In case of null cache: calls the repository for data retrieval and cache it;
			var find = await _repository.FindPartial();
			await _redis.Store<List<PartialMuseumDTO>>("partial-museum", find);
			return find;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialMuseumDTO>> PartialPaginated(int pageIndex)
		{
			//	Check for cache and return it if available;
			var cache = await _redis.Get<PaginatedResponse<PartialMuseumDTO>>($"partial-museum-{pageIndex}");
			if (cache != null) return cache;

			//	In case of null cache: calls the repository for data retrieval and cache it;
			var find = await _repository.FindPartialPaginated(pageIndex);
			await _redis.Store<PaginatedResponse<PartialMuseumDTO>>($"partial-museum-{pageIndex}", find);
			return find;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialMuseumDTO>> PaginatedQuery(MuseumQueryParams queryParams, int pageIndex)
		{
			//	Check for cache and return it if available;
			var cache = await _redis.Get<PaginatedResponse<PartialMuseumDTO>>($"query-museum-{pageIndex}");
			if (cache != null) return cache;

			//	In case of null cache: calls the repository for data retrieval and cache it;
			var find = await _repository.PaginatedQuery(queryParams, pageIndex);
			await _redis.Store<PaginatedResponse<PartialMuseumDTO>>($"query-museum-{pageIndex}", find);
			return find;
		}
		//
		//
		//
		public async Task<Museum?> FindById(int id) => await _repository.FindById(id);
		//
		//
		//
		public async Task<Museum?> FindBySlug(string slug) => await _repository.FindBySlug(slug);
		//
		//
		//
		public async Task<bool?> Delete(int id) => await _repository.DeleteById(id);
		//
		//
		//
		public async Task<Museum> Save(MuseumDTO museum) => await _repository.Save(new Museum()
		{
			Country = museum.Country,
			Name = museum.Name,
			Slug = museum.Slug,
			City = museum.City,
			State = museum.State,
			Latitude = museum.Latitude,
			Longitude = museum.Longitude,
			Inauguration = museum.Inauguration,
		});
		//
		//
		//
		public async Task<Museum?> Update(int id, UpdateMuseumDTO museum) => await _repository.UpdateById(id, museum);
	}
}