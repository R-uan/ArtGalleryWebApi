using ArtGallery.Data.DataTransferObjects.Period;
using ArtGallery.Models;

namespace ArtGallery.Interfaces.Services
{
    public interface IPeriodService
    {
        Task<bool?> Delete(int id);
        Task<List<Period>> All();
        Task<Period?> FindById(int id);
        Task<Period?> Save(PeriodDTO period);
        Task<List<PartialPeriod>> Partial();
        Task<bool> Update(int id, UpdatePeriod period);
    }
}
