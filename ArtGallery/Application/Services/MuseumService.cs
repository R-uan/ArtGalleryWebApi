using ArtGallery.DTO;
using ArtGallery.Utils;
using System.Reflection;
using ArtGallery.Models;
using ArtGallery.Utils.Caching;
using ArtGallery.Interfaces.Services;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Services
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
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
			var cached_data = await _redis.Get<List<Museum>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_all = await _repository.Find();
			await _redis.Store<List<Museum>>(cache_key, find_all);
			return find_all;
		}
		//
		//
		//
		public async Task<List<PartialMuseumDTO>> Partial()
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
			var cached_data = await _redis.Get<List<PartialMuseumDTO>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_partial = await _repository.FindPartial();
			await _redis.Store<List<PartialMuseumDTO>>(cache_key, find_partial);
			return find_partial;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialMuseumDTO>> PartialPaginated(int pageIndex)
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!, pageIndex);
			var cached_data = await _redis.Get<PaginatedResponse<PartialMuseumDTO>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_partial_paginated = await _repository.FindPartialPaginated(pageIndex);
			await _redis.Store<PaginatedResponse<PartialMuseumDTO>>(cache_key, find_partial_paginated);
			return find_partial_paginated;
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
		public async Task<bool?> Delete(int id)
		{
			var delete = await _repository.DeleteById(id);
			if (delete == true) _redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return delete;
		}
		//
		//
		//
		public async Task<Museum> Save(MuseumDTO museum)
		{
			var save = await _repository.Save(new Museum()
			{
				Country = museum.Country,
				Name = museum.Name,
				Slug = museum.Slug,
				City = museum.City,
				State = museum.State,
				Latitude = museum.Latitude,
				Longitude = museum.Longitude,
				Inauguration = museum.Inauguration,
			}) ?? throw new Exception("Failed to Save");
			_redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return save;
		}
		//
		//
		//
		public async Task<Museum?> Update(int id, UpdateMuseumDTO museum)
		{
			var update = await _repository.UpdateById(id, museum) ?? throw new Exception("Fail to update entity");
			_redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return update;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialMuseumDTO>> PaginatedQuery(MuseumQueryParams queryParams, int pageIndex)
			=> await _repository.PaginatedQuery(queryParams, pageIndex);
	}
}