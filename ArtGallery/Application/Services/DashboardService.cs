using ArtGallery.Utils;
using ArtGallery.Interfaces.Services;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Services
{
    public class DashboardService(IDashboardRepository service) : IDashboardService
    {
        private readonly IDashboardRepository _service = service;

        public async Task<DataSummary> GetSummary()
        {
            return await _service.DataSummary();
        }
    }
}
