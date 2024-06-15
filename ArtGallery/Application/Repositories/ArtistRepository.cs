using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Repositories
{
	public class ArtistRepository(GalleryDbContext db) : IArtistRepository
	{
		private readonly GalleryDbContext _db = db;

		public async Task<List<Artist>> Find()
		{
			return await _db.Artists.ToListAsync();
		}

		public async Task<List<PartialArtistDTO>> FindPartial()
		{
			return await _db.Artists.Select(artist => new PartialArtistDTO(artist.Name, artist.Slug, artist.ArtistId, artist.ImageURL)).ToListAsync();
		}

		public async Task<Artist?> FindById(int id)
		{
			return await _db.Artists.FindAsync(id);
		}

		public async Task<Artist?> FindBySlug(string slug)
		{
			return await _db.Artists.Where(artist => artist.Slug == slug).FirstOrDefaultAsync();
		}

		public async Task<Artist?> UpdateById(int id, UpdateArtistDTO patch)
		{
			var artist = await _db.Artists.FindAsync(id);
			if (artist == null) return null;
			if (patch != null)
			{
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

		public async Task<bool?> DeleteById(int id)
		{
			var artist_to_delete = await _db.Artists.FindAsync(id);
			if (artist_to_delete == null) return null;
			_db.Artists.Remove(artist_to_delete);
			await _db.SaveChangesAsync();
			var exists = await _db.Artists.FindAsync(id);
			return exists == null;
		}

		public async Task<Artist> Save(Artist artist)
		{
			if (artist == null) throw new Exception();
			var artist_entity = _db.Artists.Add(artist);
			await _db.SaveChangesAsync();
			return artist_entity.Entity;
		}
		public async Task<PaginatedResponse<PartialArtistDTO>> FindPartialPaginated(int pageIndex)
		{
			var artists = from artist in _db.Artists
										select new PartialArtistDTO
										{
											ArtistId = artist.ArtistId,
											ImageURL = artist.ImageURL,
											Name = artist.Name,
											Slug = artist.Slug
										};


			return await artists.Paginate(pageIndex);
		}

		public async Task<PaginatedResponse<PartialArtistDTO>> PaginatedQuery(ArtistQueryParams queryParams, int pageIndex)
		{
			var query = _db.Artists.AsQueryable();
			if (!string.IsNullOrEmpty(queryParams.Name))
			{ query = query.Where(a => EF.Functions.Like(a.Name.ToLower(), $"%{queryParams.Name.ToLower()}%")); }
			if (!string.IsNullOrEmpty(queryParams.Country))
			{ query = query.Where(a => EF.Functions.Like(a.Country.ToLower(), $"%{queryParams.Country.ToLower()}%")); }
			if (!string.IsNullOrEmpty(queryParams.Movement))
			{ query = query.Where(a => EF.Functions.Like(a.Movement!.ToLower(), $"%{queryParams.Movement.ToLower()}%")); }
			if (!string.IsNullOrEmpty(queryParams.Profession))
			{ query = query.Where(a => EF.Functions.Like(a.Profession!.ToLower(), $"%{queryParams.Profession.ToLower()}%")); }

			var artists = from artist in query
										select new PartialArtistDTO
										{
											Name = artist.Name,
											Slug = artist.Slug,
											ArtistId = artist.ArtistId,
											ImageURL = artist.ImageURL,
										};

			return await artists.Paginate(pageIndex);
		}
	}
}