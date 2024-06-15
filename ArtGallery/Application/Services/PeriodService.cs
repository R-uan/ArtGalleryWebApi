using ArtGallery.Models;
using ArtGallery.Repositories;
using ArtGallery.Services;

namespace ArtGallery;

public class PeriodService(IPeriodRepository repository, IRedisRepository redis) : IPeriodService
{
	private readonly IPeriodRepository _repository = repository;
	private readonly IRedisRepository _redis = redis;
	//
	//
	//
	public async Task<List<PartialPeriod>> Partial()
	{
		//	Check cache for existing data.
		var cache = await _redis.Get<List<PartialPeriod>>("partial-periods");
		if (cache != null) return cache;

		//	In case of null cache: calls the repository for data retrieval and cache it. 
		var find = await _repository.FindPartial();
		await _redis.Store<List<PartialPeriod>>("partial-periods", find);
		return find;
	}
	//
	//
	//
	public async Task<List<Period>> All()
	{
		//	Check for cache and return it if available;
		var cache = await _redis.Get<List<Period>>("all-periods");
		if (cache != null) return cache;

		//	In case of null cache: calls the repository for data retrieval and cache it;
		var find = await _repository.Find();
		await _redis.Store<List<Period>>("all-periods", find);
		return find;
	}
	//
	//
	//
	public async Task<bool?> Delete(int id) => await _repository.Delete(id);
	//
	//
	//
	public async Task<Period?> FindById(int id) => await _repository.FindById(id);
	//
	//
	//
	public async Task<Period?> Save(PeriodDTO period) => await _repository.Save(new Period()
	{
		Name = period.Name,
		Summary = period.Summary,
		Start = period.Start,
		End = period.End,
	});

}
