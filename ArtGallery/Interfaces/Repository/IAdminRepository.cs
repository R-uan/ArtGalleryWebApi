using ArtGallery.Models;

namespace ArtGallery.Interfaces.Repository
{
    public interface IAdminRepository
    {
        public Task<Admin?> FindByUsername(string username);
        public Task<int?> AddOneAdmin(Admin admin);
    }
}