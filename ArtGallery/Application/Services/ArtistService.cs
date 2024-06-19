using ArtGallery.DTO;
using ArtGallery.Utils;
using ArtGallery.Models;
using System.Reflection;
using ArtGallery.Utils.Caching;
using ArtGallery.Interfaces.Services;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Services
{
	public class ArtistService(IArtistRepository repository, IRedisRepository redis) : IArtistService
	{
		private readonly IArtistRepository _repository = repository;
		private readonly IRedisRepository _redis = redis;
		//
		//
		//
		public async Task<List<Artist>> All()
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
			var cached_data = await _redis.Get<List<Artist>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_all = await _repository.Find();
			await _redis.Store<List<Artist>>(cache_key, find_all);
			return find_all;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialArtistDTO>> PartialPaginated(int pageIndex)
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!, pageIndex);
			var cached_data = await _redis.Get<PaginatedResponse<PartialArtistDTO>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_partial_paginated = await _repository.FindPartialPaginated(pageIndex);
			await _redis.Store<PaginatedResponse<PartialArtistDTO>>(cache_key, find_partial_paginated);
			return find_partial_paginated;
		}
		//
		//
		//
		public async Task<List<PartialArtistDTO>> Partial()
		{
			var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
			var cached_data = await _redis.Get<List<PartialArtistDTO>>(cache_key);
			if (cached_data != null) return cached_data;

			var find_partial = await _repository.FindPartial();
			await _redis.Store<List<PartialArtistDTO>>(cache_key, find_partial);
			return find_partial;
		}
		//
		//
		//
		public async Task<Artist?> FindById(int id) => await _repository.FindById(id);
		//
		//
		//
		public async Task<Artist> Save(ArtistDTO artist)
		{
			var save = await _repository.Save(new Artist()
			{
				Name = artist.Name,
				Slug = artist.Slug,
				Country = artist.Country,
				Biography = artist.Biography,
				Movement = artist.Movement,
				Profession = artist.Profession,
				ImageURL = artist.ImageURL
			}) ?? throw new Exception("Fail to save");

			_redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return save;
		}
		//
		//
		//
		public async Task<Artist?> Update(int id, UpdateArtistDTO artist)
		{
			var update = await _repository.UpdateById(id, artist) ?? throw new Exception("Failed to update entity.");
			_redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
			return update;
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
		public async Task<Artist?> FindBySlug(string slug) => await _repository.FindBySlug(slug);
		//
		//
		//
		public async Task<PaginatedResponse<PartialArtistDTO>> PaginatedQuery(ArtistQueryParams queryParams, int page)
			=> await _repository.PaginatedQuery(queryParams, page);
	}
}