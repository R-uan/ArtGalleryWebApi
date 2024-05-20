using ArtGallery.Interfaces;
using ArtGallery.Models;
using ArtGallery.Repositories;
using ArtGallery.Utils;

namespace ArtGallery.Services {
    public class AdminService(IAdminRepository repository) : IAdminService {
        private readonly IAdminRepository _repository = repository;
        public async Task<string?> Authenticate(string username, string password) {
            Admin? admin = await _repository.FindByUsername(username);
            if (admin == null) return null;
            bool valid = BC.Verify(password, admin.Password);
            if (!valid) return null;
            return AuthHelper.GenerateJWTToken(admin);
        }

        public async Task<int?> Register(string username, string password) { 
            string hashed_password = BC.HashPassword(password);
            Admin admin = new() { Username = username, Password = hashed_password };
            return await _repository.AddOneAdmin(admin);
        }
    }
}