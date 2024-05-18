using ArtGallery.Interfaces;
using ArtGallery.Models;
using ArtGallery.Repositories;
using BCrypt.Net;

namespace ArtGallery.Services {
    public class AdminService(IAdminRepository repository) : IAdminService {
        private readonly IAdminRepository _repository = repository;
        public async Task<string?> Authenticate(string username, string password) {
            Admin? admin = await _repository.FindByUsername(username);
            if (admin == null) return null;
            bool valid = BCrypt.Net.BCrypt.Verify(password, admin.Password);
            if (!valid) return null;
            return "token here";
        }
    }
}