using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace ArtGallery.Repositories {
	public class ArtworkRepository(GalleryDbContext db) : IArtworkRepository {
		private readonly GalleryDbContext _db = db;

		public async Task<bool?> DeleteById(int id) {
			var artwork = await _db.Artworks.FindAsync(id);
			if (artwork == null) return null;
			_db.Artworks.Remove(artwork);
			await _db.SaveChangesAsync();
			var verify = await _db.Artworks.FindAsync(id);
			return verify == null;
		}

		public async Task<List<Artwork>> FindAll() {
			return await _db.Artworks.ToListAsync();
		}

		public async Task<List<PartialArtwork>> FindAllPartial() {
			return await _db.Artworks.Join(_db.Artists,
				artwork => artwork.ArtistId, artist => artist.ArtistId,
				(artwork, artist) => new PartialArtwork(artwork.Title, artwork.Slug, artwork.ImageURL, artist.Name)).ToListAsync();
		}

		public async Task<Artwork?> FindById(int id) {
			return await _db.Artworks.FindAsync(id);
		}

		public async Task<Artwork?> FindBySlug(string slug) {
			return await _db.Artworks
			.Include(artwork => artwork.Artist)
			.Include(artwork => artwork.Museum)
			.Where(a => a.Slug == slug)
			.FirstOrDefaultAsync();
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
				if (!string.IsNullOrEmpty(patch.History)) artwork.History = patch.History;
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