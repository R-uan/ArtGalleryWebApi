using ArtGallery.Models;

namespace ArtGallery.Repositories;

public interface IArtistRepository {
	Task<Artist> SaveOne(Artist artist);
	Task<bool> DeleteById(int id);
	Task<bool> UpdateById(int id, Artist updated_artist);

	Task<Artist> FindById(int id);
	Task<Artist> FindBySlug(string slug);

	Task<List<Artist>> FindAll();
	Task<List<ArtistPartial>> FindAllPartial();
}
