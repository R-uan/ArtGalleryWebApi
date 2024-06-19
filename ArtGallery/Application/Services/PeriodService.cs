using System.Reflection;
using ArtGallery.Models;
using ArtGallery.Utils.Caching;
using ArtGallery.Interfaces.Services;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Services
{
    public class PeriodService(IPeriodRepository repository, IRedisRepository redis) : IPeriodService
    {
        private readonly IPeriodRepository _repository = repository;
        private readonly IRedisRepository _redis = redis;
        //
        //
        //
        public async Task<List<PartialPeriod>> Partial()
        {
            var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
            var cached_data = await _redis.Get<List<PartialPeriod>>(cache_key);
            if (cached_data != null) return cached_data;

            var find_partial = await _repository.FindPartial();
            await _redis.Store<List<PartialPeriod>>(cache_key, find_partial);
            return find_partial;
        }
        //
        //
        //
        public async Task<List<Period>> All()
        {
            var cache_key = CacheKeyHelper.GenerateCacheKey(MethodBase.GetCurrentMethod()!);
            var cached_data = await _redis.Get<List<Period>>(cache_key);
            if (cached_data != null) return cached_data;

            var find_all = await _repository.Find();
            await _redis.Store<List<Period>>(cache_key, find_all);
            return find_all;
        }
        //
        //
        //
        public async Task<bool?> Delete(int id)
        {
            var delete = await _repository.Delete(id);
            if (delete == true) _redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
            return delete;
        }
        //
        //
        //
        public async Task<Period?> FindById(int id) => await _repository.FindById(id);
        //
        //
        //
        public async Task<Period?> Save(PeriodDTO period)
        {
            var save = await _repository.Save(new Period()
            {
                Name = period.Name,
                Summary = period.Summary,
                Start = period.Start,
                End = period.End,
            }) ?? throw new Exception("");

            _redis.ClearThisKeys(MethodBase.GetCurrentMethod()!);
            return save;
        }
    }
}