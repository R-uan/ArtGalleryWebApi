using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories {
	public class MuseumRepository(GalleryDbContext db) : IRepository<Museum, UpdateMuseum, MuseumPartial> {
		private readonly GalleryDbContext _db = db;

		public async Task<Museum> SaveOne(Museum museum) {
			if (museum == null) throw new Exception();
			var museum_entity = await _db.AddAsync(museum);
			await _db.SaveChangesAsync();
			return museum_entity.Entity;
		}

		public async Task<List<Museum>> FindAll() {
			return await _db.Museums.ToListAsync();
		}

		public async Task<List<MuseumPartial>> FindAllPartial() {
			return await _db.Museums.Select(m => new MuseumPartial(m.Name, m.Country)).ToListAsync();
		}

		public async Task<Museum?> FindById(int id) {
			return await _db.Museums.FindAsync(id);
		}

		public async Task<Museum?> FindBySlug(string slug) {
			return await _db.Museums.Where(museum => museum.Slug == slug).FirstAsync();
		}

		public async Task<Museum?> UpdateById(int id, UpdateMuseum patch) {
			var museum = await _db.Museums.FindAsync(id);
			if (museum == null) return null;
			if (patch != null) {
				if (!string.IsNullOrEmpty(patch.Name)) museum.Name = patch.Name;
				if (!string.IsNullOrEmpty(patch.City)) museum.City = patch.City;
				if (!string.IsNullOrEmpty(patch.State)) museum.State = patch.State;
				if (!string.IsNullOrEmpty(patch.Country)) museum.Country = patch.Country;

				if (patch.Inauguration.HasValue) museum.Inauguration = patch.Inauguration;
				if (patch.Latitude.HasValue) museum.Latitude = patch.Latitude;
				if (patch.Longitude.HasValue) museum.Longitude = patch.Longitude;

				await _db.SaveChangesAsync();
				return await _db.Museums.FindAsync(id);
			}
			throw new Exception();
		}

		public async Task<bool?> DeleteById(int id) {
			var museum = await _db.Museums.FindAsync(id);
			if (museum == null) return null;
			_db.Museums.Remove(museum);
			await _db.SaveChangesAsync();
			var exists = await _db.Museums.AnyAsync(a => a.MuseumId == id);
			return !exists;
		}
	}
}