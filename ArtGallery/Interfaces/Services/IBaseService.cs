using ArtGallery.Utils;

namespace ArtGallery.Interfaces {
	public interface IBaseService<TEntity, TEntityDTO, UEntity, PEntity> {
		Task<TEntity> PostOne(TEntityDTO artist);
		Task<TEntity?> GetOneById(int id);
		Task<TEntity?> GetOneBySlug(string slug);

		Task<List<TEntity>> GetAll();
		Task<List<PEntity>> GetAllPartial();

		Task<bool?> DeleteOne(int id);
		Task<TEntity?> UpdateOne(int id, UEntity artist);
		Task<PaginatedResponse<PEntity>> GetAllPartialPaginated(int pageIndex);

	}
}