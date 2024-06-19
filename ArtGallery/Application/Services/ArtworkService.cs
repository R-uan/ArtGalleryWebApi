using ArtGallery.DTO;
using ArtGallery.Utils;
using ArtGallery.Models;
using System.Reflection;
using ArtGallery.Utils.Caching;
using ArtGallery.Interfaces.Services;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Services
{
	public class ArtworkService(IArtworkRepository repository, IRedisRepository redis) : IArtworkService
	{
		private readonly IArtworkRepository _repository = repository;
		private readonly IRedisRepository _redis = redis;
		//
		//
		//
		public async Task<List<Artwork>> All()
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
			var cached_data = await _redis.Get<List<Artwork>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_all = await _repository.Find();
			await _redis.Store<List<Artwork>>(cache_key, find_all);
			return find_all;
		}
		//
		//
		//
		public async Task<List<PartialArtworkDTO>> Partial()
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
			var cached_data = await _redis.Get<List<PartialArtworkDTO>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_partial = await _repository.FindPartial();
			await _redis.Store<List<PartialArtworkDTO>>(cache_key, find_partial);
			return find_partial;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialArtworkDTO>> PartialPaginated(int pageIndex)
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!, pageIndex);
			var cached_data = await _redis.Get<PaginatedResponse<PartialArtworkDTO>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_partial_paginated = await _repository.FindPartialPaginated(pageIndex);
			await _redis.Store<PaginatedResponse<PartialArtworkDTO>>(cache_key, find_partial_paginated);
			return find_partial_paginated;
		}
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
		public async Task<Artwork?> FindById(int id) => await _repository.FindById(id);
		//
		//
		//
		public async Task<Artwork?> FindBySlug(string slug) => await _repository.FindBySlug(slug);
		//
		//
		//
		public async Task<Artwork> Save(ArtworkDTO artwork)
		{
			var save = await _repository.Save(new Artwork()
			{
				Title = artwork.Title,
				ArtistId = artwork.ArtistId,
				History = artwork.History,
				Slug = artwork.Slug,
				ImageURL = artwork.ImageURL,
				PeriodId = artwork.PeriodId,
				MuseumId = artwork.MuseumId,
				Year = artwork.Year
			}) ?? throw new Exception("Fail to save");

			_redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return save;
		}
		//
		//
		//
		public async Task<Artwork?> Update(int id, UpdateArtworkDTO artist)
		{
			var update = await _repository.UpdateById(id, artist) ?? throw new Exception("Fail to update");
			_redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return update;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialArtworkDTO>> PaginatedQuery(ArtworkQueryParams queryParams, int pageIndex)
			=> await _repository.PaginatedQuery(queryParams, pageIndex);
	}
}