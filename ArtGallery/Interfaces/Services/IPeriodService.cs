using ArtGallery.Models;
namespace ArtGallery.Services;

public interface IPeriodService {
	Task<bool?> DeletePeriod(int id);
	Task<List<Period>> GetPeriods();
	Task<Period?> GetOnePeriod(int id);
	Task<Period?> PostPeriod(PeriodDTO period);
	Task<List<PartialPeriod>> GetPartialPeriods();
}
