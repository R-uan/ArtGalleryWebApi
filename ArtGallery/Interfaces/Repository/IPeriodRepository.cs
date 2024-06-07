namespace ArtGallery.Repositories;
using ArtGallery.Models;

public interface IPeriodRepository {
	Task<bool?> Delete(int id);
	Task<List<Period>> Find();
	Task<Period?> FindById(int id);
	Task<Period?> Save(Period period);
	Task<List<PartialPeriod>> FindPartial();
}
