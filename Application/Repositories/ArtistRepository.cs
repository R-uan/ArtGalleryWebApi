using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories {
	public class ArtistRepository(GalleryDbContext db) : IRepository<Artist, UpdateArtist, ArtistPartial> {
		private readonly GalleryDbContext _db = db;

		public async Task<List<Artist>> FindAll() {
			return await _db.Artists.ToListAsync();
		}

		public async Task<List<ArtistPartial>> FindAllPartial() {
			return await _db.Artists.Select(artist => new ArtistPartial(artist.Name, artist.Slug)).ToListAsync();
		}

		public async Task<Artist?> FindById(int id) {
			return await _db.Artists.FindAsync(id);
		}

		public async Task<Artist?> FindBySlug(string slug) {
			return await _db.Artists.Where(artist => artist.Slug == slug).FirstAsync();
		}

		public async Task<bool?> UpdateById(int id, UpdateArtist patch) {
			var artist = await _db.Artists.FindAsync(id);
			if (artist == null) return null;
			if (patch != null) {
				if (!string.IsNullOrEmpty(patch.Name)) artist.Name = patch.Name;
				if (!string.IsNullOrEmpty(patch.Country)) artist.Country = patch.Country;
				if (!string.IsNullOrEmpty(patch.Slug)) artist.Slug = patch.Slug;
				if (!string.IsNullOrEmpty(patch.Description)) artist.Description = patch.Description;

				if (patch.Date_of_birth != null) artist.Date_of_birth = patch.Date_of_birth;
				if (patch.Date_of_death != null) artist.Date_of_death = patch.Date_of_death;

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