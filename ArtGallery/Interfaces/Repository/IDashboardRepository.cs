using ArtGallery.Utils;

namespace ArtGallery.Interfaces.Repository
{
    public interface IDashboardRepository
    {
        Task<DataSummary> DataSummary();
    }
}
