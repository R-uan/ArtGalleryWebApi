using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces;

namespace ArtGallery.Repositories {
	public class ArtworkRepository(GalleryDbContext db) : IArtworkRepository {
		private readonly GalleryDbContext _db = db;

		public Task<bool?> DeleteById(int id) {
			throw new NotImplementedException();
		}

		public async Task<List<Artwork>> FindAll() {
			return await _db.Artworks.ToListAsync();
		}

		public async Task<List<PartialArtwork>> FindAllPartial() {
			return await _db.Artworks.Join(_db.Artists,
				a => a.ArtistId, b => b.ArtistId,
				(a, b) => new PartialArtwork(a.Title, a.Slug, a.ImageURL, b.Name)).ToListAsync();
		}

		public async Task<Artwork?> FindById(int id) {
			return await _db.Artworks.FindAsync(id);
		}

		public async Task<Artwork?> FindBySlug(string slug) {
			return await _db.Artworks.Where(a => a.Slug == slug).FirstAsync();
		}

		public async Task<Artwork> SaveOne(Artwork entity) {
			var artwork = await _db.Artworks.AddAsync(entity);
			await _db.SaveChangesAsync();
			return artwork.Entity;
		}

		public async Task<Artwork?> UpdateById(int id, UpdateArtwork patch) {
			var artwork = await _db.Artworks.FindAsync(id);
			if (artwork == null) return null;
			else if (patch != null) {
				if (!string.IsNullOrEmpty(patch.Title)) artwork.Title = patch.Title;
				if (!string.IsNullOrEmpty(patch.Description)) artwork.Description = patch.Description;
				if (!string.IsNullOrEmpty(patch.Slug)) artwork.Slug = patch.Slug;
				if (!string.IsNullOrEmpty(patch.ImageURL)) artwork.ImageURL = patch.ImageURL;

				if (patch.Year.HasValue) artwork.Year = patch.Year;
				if (patch.ArtistId.HasValue && patch.ArtistId != null) artwork.ArtistId = (int)patch.ArtistId;
				if (patch.MuseumId.HasValue && patch.MuseumId != null) artwork.MuseumId = (int)patch.MuseumId;

				await _db.SaveChangesAsync();
				return await _db.Artworks.FindAsync(id);
			}

			throw new Exception();
		}
	}
}