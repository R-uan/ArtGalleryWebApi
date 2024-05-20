using ArtGallery.Models;

namespace ArtGallery.Interfaces {
    public interface IAdminService {
        public Task<string?> Authenticate(string username, string password);
        public Task<int?> Register(string username, string password);
    }
}