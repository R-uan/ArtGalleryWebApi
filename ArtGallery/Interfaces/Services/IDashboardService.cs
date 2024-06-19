using ArtGallery.Utils;

namespace ArtGallery.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DataSummary> GetSummary();
    }
}
