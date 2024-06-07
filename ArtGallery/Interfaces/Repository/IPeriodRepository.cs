namespace ArtGallery.Repositories;
using ArtGallery.Models;


public interface IPeriodRepository {
	Task<bool?> DeletePeriod(int id);
	Task<List<Period>> FindPeriods();
	Task<Period?> FindOnePeriod(int id);
	Task<Period?> SavePeriod(Period period);
	Task<List<PartialPeriod>> FindPartialPeriods();
}
