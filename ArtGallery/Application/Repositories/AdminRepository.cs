using ArtGallery.Interfaces;
using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories {
    public class AdminRepository(GalleryDbContext context) : IAdminRepository {
        private readonly GalleryDbContext _db = context;
        public async Task<Admin?> FindByUsername(string username) {
            return await _db.Admin.Where(admin => admin.Username == username).FirstOrDefaultAsync();
        }
    }
}