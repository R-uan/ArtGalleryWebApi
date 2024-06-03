using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Repositories {
	public class MuseumRepository(GalleryDbContext db) : IMuseumRepository {
		private readonly GalleryDbContext _db = db;

		// Check IBaseRepository for the documentation of the methods.

		public async Task<Museum> SaveOne(Museum museum) {
			if (museum == null) throw new Exception();
			var museum_entity = await _db.AddAsync(museum);
			await _db.SaveChangesAsync();
			return museum_entity.Entity;
		}

		public async Task<List<Museum>> FindAll() {
			return await _db.Museums.ToListAsync();
		}

		public async Task<List<PartialMuseumDTO>> FindAllPartial() {
			return await _db.Museums.Select(m => new PartialMuseumDTO(m.MuseumId, m.Name, m.Country, m.Slug)).ToListAsync();
		}

		public async Task<Museum?> FindById(int id) {
			return await _db.Museums.FindAsync(id);
		}

		public async Task<Museum?> FindBySlug(string slug) {
			return await _db.Museums.Where(museum => museum.Slug == slug).FirstOrDefaultAsync();
		}

		public async Task<Museum?> UpdateById(int id, UpdateMuseumDTO patch) {
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
			var exists = await _db.Museums.FindAsync(id);
			return exists == null;
		}

		public async Task<PaginatedResponse<PartialMuseumDTO>> FindAllPartialPaginated(int page_index, int page_size) {
			var museums = await _db.Museums
							.OrderBy(m => m.MuseumId)
							.Skip((page_index - 1) * page_size)
							.Take(page_size)
							.Select(m => new PartialMuseumDTO() {
								MuseumId = m.MuseumId,
								Slug = m.Slug,
								Name = m.Name,
								Country = m.Country,
							}).ToListAsync();

			var count = await _db.Museums.CountAsync();
			int total_pages = (int)Math.Ceiling(count / (double)page_size);
			return new PaginatedResponse<PartialMuseumDTO>(museums, page_index, total_pages);
		}
	}
}