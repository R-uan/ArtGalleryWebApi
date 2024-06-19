using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Repositories
{
	public class AdminRepository(GalleryDbContext context) : IAdminRepository
	{
		private readonly GalleryDbContext _db = context;

		public async Task<int?> AddOneAdmin(Admin admin)
		{
			var add = await _db.Admin.AddAsync(admin);
			await _db.SaveChangesAsync();
			return add.Entity.AdminId;
		}

		public async Task<Admin?> FindByUsername(string username)
		{
			return await _db.Admin.Where(admin => admin.Username == username).FirstOrDefaultAsync();
		}
	}
}