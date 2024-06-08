using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.Utils;
using ArtGallery.DTO;
using System.Text.Json;
namespace ArtGallery.Services;

public class ArtworkService(IArtworkRepository repository, IRedisRepository redis) : IArtworkService {
	private readonly IArtworkRepository _repository = repository;
	private readonly IRedisRepository _redis = redis;

	public async Task<PaginatedResponse<PartialArtworkDTO>> PaginatedQuery(ArtworkQueryParams queryParams, int page) {
		return await _repository.PaginatedQuery(queryParams, page);
	}

	public async Task<List<Artwork>> All() {
		var cached_artworks = await _redis.Get<Artwork>("artwork-all");
		if (cached_artworks == null) {
			var artworks = await _repository.Find();
			await _redis.Store("artwork-all", artworks);
			return artworks;
		} else { return cached_artworks; }
	}

	public async Task<List<PartialArtworkDTO>> Partial() {
		return await _repository.FindPartial();
	}

	public async Task<PaginatedResponse<PartialArtworkDTO>> PartialPaginated(int pageIndex) {
		return await _repository.FindPartialPaginated(pageIndex);
	}

	public async Task<Artwork?> FindById(int id) {
		return await _repository.FindById(id);
	}

	public async Task<Artwork?> FindBySlug(string slug) {
		return await _repository.FindBySlug(slug);
	}

	public async Task<Artwork> Save(ArtworkDTO artwork) {
		Artwork mapping = new() {
			Title = artwork.Title,
			ArtistId = artwork.ArtistId,
			History = artwork.History,
			Slug = artwork.Slug,
			ImageURL = artwork.ImageURL,
			PeriodId = artwork.PeriodId,
			MuseumId = artwork.MuseumId,
			Year = artwork.Year
		};
		return await _repository.Save(mapping);
	}

	public async Task<Artwork?> Update(int id, UpdateArtworkDTO artist) {
		return await _repository.UpdateById(id, artist);
	}

	public async Task<bool?> Delete(int id) {
		return await _repository.DeleteById(id);
	}
}

