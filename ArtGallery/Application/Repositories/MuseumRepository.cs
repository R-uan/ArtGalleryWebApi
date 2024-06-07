using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Repositories {
	public class MuseumRepository(GalleryDbContext db) : IMuseumRepository {
		private readonly GalleryDbContext _db = db;

		// Check IBaseRepository for the documentation of the methods.

		public async Task<Museum> Save(Museum museum) {
			if (museum == null) throw new Exception();
			var museum_entity = await _db.AddAsync(museum);
			await _db.SaveChangesAsync();
			return museum_entity.Entity;
		}

		public async Task<List<Museum>> Find() {
			return await _db.Museums.ToListAsync();
		}

		public async Task<List<PartialMuseumDTO>> FindPartial() {
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

		public async Task<PaginatedResponse<PartialMuseumDTO>> FindPartialPaginated(int page_index) {
			var museums = from museum in _db.Museums
										select new PartialMuseumDTO {
											Country = museum.Country,
											MuseumId = museum.MuseumId,
											Name = museum.Name,
											Slug = museum.Slug,
										};
			return await museums.Paginate(page_index);
		}

		public async Task<PaginatedResponse<PartialMuseumDTO>> PaginatedQuery(MuseumQueryParams queryParams, int pageIndex) {
			var query = _db.Museums.AsQueryable();
			if (!string.IsNullOrEmpty(queryParams.Name)) {
				query = query.Where(m => EF.Functions.ILike(m.Name, $"%{queryParams.Name}%"));
			}
			if (!string.IsNullOrEmpty(queryParams.City)) {
				query = query.Where(m => EF.Functions.ILike(m.City!, $"%{queryParams.City}%"));
			}
			if (!string.IsNullOrEmpty(queryParams.Country)) {
				query = query.Where(m => EF.Functions.ILike(m.Country, $"%{queryParams.Country}%"));
			}
			if (!string.IsNullOrEmpty(queryParams.State)) {
				query = query.Where(m => EF.Functions.ILike(m.State!, $"%{queryParams.State}%"));
			}

			var museums = from museum in query
										select new PartialMuseumDTO {
											Country = museum.Country,
											MuseumId = museum.MuseumId,
											Name = museum.Name,
											Slug = museum.Slug
										};

			return await museums.Paginate(pageIndex);
		}
	}
}