using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.Utils;
using ArtGallery.DTO;
using System.Text.Json;
namespace ArtGallery.Services;

public class ArtworkService(IArtworkRepository repository, IRedisRepository redis) : IArtworkService
{
	private readonly IArtworkRepository _repository = repository;
	private readonly IRedisRepository _redis = redis;
	//
	//
	//
	public async Task<List<Artwork>> All()
	{
		//	Check for cache and return it if available;
		var cache = await _redis.Get<List<Artwork>>("all-artwork");
		if (cache != null) return cache;

		//	In case of null cache: calls the repository for data retrieval and cache it;
		var find = await _repository.Find();
		await _redis.Store<List<Artwork>>("all-artwork", find);
		return find;
	}
	//
	//
	//
	public async Task<List<PartialArtworkDTO>> Partial()
	{
		//	Check for cache and return it if available;
		var cache = await _redis.Get<List<PartialArtworkDTO>>("partial-artwork");
		if (cache != null) return cache;

		//	In case of null cache: calls the repository for data retrieval and cache it;
		var find = await _repository.FindPartial();
		await _redis.Store<List<PartialArtworkDTO>>("partial-artwork", find);
		return find;
	}
	//
	//
	//
	public async Task<PaginatedResponse<PartialArtworkDTO>> PartialPaginated(int pageIndex)
	{
		//	Check for cache and return it if available;
		var cache = await _redis.Get<PaginatedResponse<PartialArtworkDTO>>($"partial-paginated-{pageIndex}");
		if (cache != null) return cache;

		//	In case of null cache: calls the repository for data retrieval and cache it;
		var find = await _repository.FindPartialPaginated(pageIndex);
		await _redis.Store<PaginatedResponse<PartialArtworkDTO>>($"partial-paginated-{pageIndex}", find);
		return find;
	}
	//
	//
	//
	public async Task<bool?> Delete(int id) => await _repository.DeleteById(id);
	//
	//
	//
	public async Task<Artwork?> FindById(int id) => await _repository.FindById(id);
	//
	//
	//
	public async Task<Artwork?> FindBySlug(string slug) => await _repository.FindBySlug(slug);
	//
	//
	//
	public async Task<Artwork> Save(ArtworkDTO artwork) => await _repository.Save(new Artwork()
	{
		Title = artwork.Title,
		ArtistId = artwork.ArtistId,
		History = artwork.History,
		Slug = artwork.Slug,
		ImageURL = artwork.ImageURL,
		PeriodId = artwork.PeriodId,
		MuseumId = artwork.MuseumId,
		Year = artwork.Year
	});
	//
	//
	//
	public async Task<Artwork?> Update(int id, UpdateArtworkDTO artist) => await _repository.UpdateById(id, artist);
	//
	//
	//
	public async Task<PaginatedResponse<PartialArtworkDTO>> PaginatedQuery(ArtworkQueryParams queryParams, int pageIndex)
	=> await _repository.PaginatedQuery(queryParams, pageIndex);
}

