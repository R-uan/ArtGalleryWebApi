using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Services
{
	public class ArtistService(IArtistRepository repository, IRedisRepository redis) : IArtistService
	{
		private readonly IArtistRepository _repository = repository;
		private readonly IRedisRepository _redis = redis;
		//
		//
		//
		public async Task<List<Artist>> All()
		{
			var cache = await _redis.Get<List<Artist>>($"all-artist");
			if (cache != null) return cache;

			var find = await _repository.Find();
			await _redis.Store<List<Artist>>($"all-artist", find);
			return find;
		}
		//
		//
		//
		public async Task<PaginatedResponse<PartialArtistDTO>> PartialPaginated(int pageIndex)
		{
			var cache = await _redis.Get<PaginatedResponse<PartialArtistDTO>>($"partial-artist-{pageIndex}");
			if (cache != null) return cache;

			var find = await _repository.FindPartialPaginated(pageIndex);
			await _redis.Store<PaginatedResponse<PartialArtistDTO>>($"partial-artist-{pageIndex}", find);
			return find;
		}
		//
		//
		//
		public async Task<List<PartialArtistDTO>> Partial()
		{
			var cache = await _redis.Get<List<PartialArtistDTO>>("partial-artist");
			if (cache != null) return cache;

			var find = await _repository.FindPartial();
			await _redis.Store<List<PartialArtistDTO>>("partial-artist", find);
			return find;
		}
		//
		//
		//
		public async Task<Artist?> FindById(int id) => await _repository.FindById(id);
		//
		//
		//
		public async Task<Artist> Save(ArtistDTO artist) => await _repository.Save(new Artist()
		{
			Name = artist.Name,
			Slug = artist.Slug,
			Country = artist.Country,
			Biography = artist.Biography,
			Movement = artist.Movement,
			Profession = artist.Profession,
			ImageURL = artist.ImageURL
		});
		//
		//
		//
		public async Task<Artist?> Update(int id, UpdateArtistDTO artist) => await _repository.UpdateById(id, artist);
		//
		//
		//
		public async Task<bool?> Delete(int id) => await _repository.DeleteById(id);
		//
		//
		//
		public async Task<Artist?> FindBySlug(string slug) => await _repository.FindBySlug(slug);
		//
		//
		//
		public async Task<PaginatedResponse<PartialArtistDTO>> PaginatedQuery(ArtistQueryParams queryParams, int page)
			=> await _repository.PaginatedQuery(queryParams, page);

	}
}