using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Repositories {
	public class ArtistRepository(GalleryDbContext db) : IArtistRepository {
		private readonly GalleryDbContext _db = db;

		public async Task<List<Artist>> FindAll() {
			return await _db.Artists.ToListAsync();
		}

		public async Task<List<PartialArtistDTO>> FindAllPartial() {
			return await _db.Artists.Select(artist => new PartialArtistDTO(artist.Name, artist.Slug, artist.ArtistId, artist.ImageURL)).ToListAsync();
		}

		public async Task<Artist?> FindById(int id) {
			return await _db.Artists.FindAsync(id);
		}

		public async Task<Artist?> FindBySlug(string slug) {
			return await _db.Artists.Where(artist => artist.Slug == slug).FirstOrDefaultAsync();
		}

		public async Task<Artist?> UpdateById(int id, UpdateArtistDTO patch) {
			var artist = await _db.Artists.FindAsync(id);
			if (artist == null) return null;
			if (patch != null) {
				if (!string.IsNullOrEmpty(patch.Name)) artist.Name = patch.Name;
				if (!string.IsNullOrEmpty(patch.Country)) artist.Country = patch.Country;
				if (!string.IsNullOrEmpty(patch.Slug)) artist.Slug = patch.Slug;
				if (!string.IsNullOrEmpty(patch.Biography)) artist.Biography = patch.Biography;
				if (!string.IsNullOrEmpty(patch.ImageURL)) artist.ImageURL = patch.ImageURL;

				await _db.SaveChangesAsync();
				return await _db.Artists.FindAsync(id);
			}
			throw new Exception();
		}

		public async Task<bool?> DeleteById(int id) {
			var artist_to_delete = await _db.Artists.FindAsync(id);
			if (artist_to_delete == null) return null;
			_db.Artists.Remove(artist_to_delete);
			await _db.SaveChangesAsync();
			var exists = await _db.Artists.FindAsync(id);
			return exists == null;
		}

		public async Task<Artist> SaveOne(Artist artist) {
			if (artist == null) throw new Exception();
			var artist_entity = _db.Artists.Add(artist);
			await _db.SaveChangesAsync();
			return artist_entity.Entity;
		}

		public async Task<PaginatedResponse<PartialArtistDTO>> FindAllPartialPaginated(int page_index, int page_size) {
			var artworks = await _db.Artists
				.OrderBy(artist => artist.ArtistId)
				.Skip((page_index - 1) * page_size)
				.Take(page_size)
				.Select(artist => new PartialArtistDTO() { Name = artist.Name, ArtistId = artist.ArtistId, Slug = artist.Slug })
				.ToListAsync();

			var count = await _db.Artists.CountAsync();
			int total_pages = (int)Math.Ceiling(count / (double)page_size);
			return new PaginatedResponse<PartialArtistDTO>(artworks, page_index, total_pages);
		}
	}
}