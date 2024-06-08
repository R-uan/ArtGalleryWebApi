using ArtGallery.Models;
using ArtGallery.Repositories;
using ArtGallery.Services;

namespace ArtGallery;

public class PeriodService(IPeriodRepository repository) : IPeriodService {
	private readonly IPeriodRepository _repository = repository;
	public async Task<bool?> Delete(int id) {
		return await _repository.Delete(id);
	}

	public async Task<Period?> FindById(int id) {
		return await _repository.FindById(id);
	}

	public async Task<List<PartialPeriod>> Partial() {
		return await _repository.FindPartial();
	}

	public async Task<List<Period>> All() {
		return await _repository.Find();
	}

	public async Task<Period?> Save(PeriodDTO period) {
		var period_map = new Period() {
			Name = period.Name,
			Summary = period.Summary,
			Start = period.Start,
			End = period.End,
		};
		return await _repository.Save(period_map);
	}
}
