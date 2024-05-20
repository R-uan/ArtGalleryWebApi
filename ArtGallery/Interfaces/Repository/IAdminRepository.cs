using ArtGallery.Models;

namespace ArtGallery.Interfaces {
    public interface IAdminRepository {
        public Task<Admin?> FindByUsername(string username);
        public Task<int?> AddOneAdmin(Admin admin);
    }
}