using ArtGallery.Models;
using ArtGallery.Repositories;
using ArtGallery.Services;

namespace ArtGallery;

public class PeriodService(IPeriodRepository repository) : IPeriodService {
	private readonly IPeriodRepository _repository = repository;
	public async Task<bool?> DeletePeriod(int id) {
		return await _repository.DeletePeriod(id);
	}

	public async Task<Period?> GetOnePeriod(int id) {
		return await _repository.FindOnePeriod(id);
	}

	public async Task<List<PartialPeriod>> GetPartialPeriods() {
		return await _repository.FindPartialPeriods();
	}

	public async Task<List<Period>> GetPeriods() {
		return await _repository.FindPeriods();
	}

	public async Task<Period?> PostPeriod(PeriodDTO period) {
		var period_map = new Period() {
			Name = period.Name,
			Summary = period.Summary,
			Start = period.Start,
			End = period.End,
		};
		return await _repository.SavePeriod(period_map);
	}
}
