using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories {
	public class ArtistRepository(GalleryDbContext db) : IArtistRepository {
		private readonly GalleryDbContext _db = db;

		public Task<List<Artist>> FindAll() {
			return _db.Artists.ToListAsync();
		}

		public async Task<List<ArtistPartial>> FindAllPartial() {
			return await _db.Artists.Select(artist => new ArtistPartial(artist.Name, artist.Slug)).ToListAsync();
		}

		public async Task<Artist?> FindById(int id) {
			return await _db.Artists.FindAsync(id) ?? null;
		}

		public async Task<Artist?> FindBySlug(string slug) {
			return await _db.Artists.FirstOrDefaultAsync(artist => artist.Slug == slug) ?? null;
		}

		public async Task<bool?> UpdateById(int id, Artist updated_artist) {
			var artist_to_update = await _db.Artists.FindAsync(id);
			if (artist_to_update == null) return null;
			if (updated_artist != null) {
				if (!string.IsNullOrEmpty(updated_artist.Name)) artist_to_update.Name = updated_artist.Name;
				if (!string.IsNullOrEmpty(updated_artist.Country)) artist_to_update.Country = updated_artist.Country;
				if (!string.IsNullOrEmpty(updated_artist.Slug)) artist_to_update.Slug = updated_artist.Slug;
				if (!string.IsNullOrEmpty(updated_artist.Description)) artist_to_update.Description = updated_artist.Description;

				if (updated_artist.Date_of_birth != null) artist_to_update.Date_of_birth = updated_artist.Date_of_birth;
				if (updated_artist.Date_of_death != null) artist_to_update.Date_of_death = updated_artist.Date_of_death;

				await _db.SaveChangesAsync();
				return true;
			}

			return false;
		}

		public async Task<bool?> DeleteById(int id) {
			var artist_to_delete = await _db.Artists.FindAsync(id);
			if (artist_to_delete == null) return null;
			_db.Artists.Remove(artist_to_delete);
			await _db.SaveChangesAsync();
			var exists = await _db.Artists.AnyAsync(a => a.ArtistId == id);
			return !exists;
		}

		public async Task<Artist> SaveOne(Artist artist) {
			if (artist == null) throw new Exception();
			var artist_entity = _db.Artists.Add(artist);
			await _db.SaveChangesAsync();
			return artist_entity.Entity;
		}
	}
}