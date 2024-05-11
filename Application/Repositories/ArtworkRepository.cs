using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
namespace ArtGallery;
public class ArtworkRepository : IRepository<Artwork, UpdateArtwork, PartialArtwork> {
	private readonly GalleryDbContext _db;
	public ArtworkRepository(GalleryDbContext db) {
		_db = db;
	}

	public Task<bool?> DeleteById(int id) {
		throw new NotImplementedException();
	}

	public Task<List<Artwork>> FindAll() {

		throw new NotImplementedException();
	}

	public async Task<List<PartialArtwork>> FindAllPartial() {
		List<PartialArtwork> artists = await _db.Artworks.Join(_db.Artists,
			a => a.ArtistId, b => b.ArtistId,
			(a, b) => new PartialArtwork(a.Title, a.Slug, a.ImageURL, b.Name)).ToListAsync();
		return artists;
	}

	public Task<Artwork?> FindById(int id) {
		throw new NotImplementedException();
	}

	public Task<Artwork?> FindBySlug(string slug) {
		throw new NotImplementedException();
	}

	public Task<Artwork> SaveOne(Artwork TEntity) {
		throw new NotImplementedException();
	}

	public Task<bool?> UpdateById(int id, UpdateArtwork patch) {
		throw new NotImplementedException();
	}
}
