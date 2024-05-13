namespace ArtGallery.Interfaces {
	public interface IBaseRepository<TEntity, UEntity, PEntity> {
		Task<TEntity> SaveOne(TEntity entity);
		Task<bool?> DeleteById(int id);
		Task<TEntity?> UpdateById(int id, UEntity patch);

		Task<TEntity?> FindById(int id);
		Task<TEntity?> FindBySlug(string slug);

		Task<List<TEntity>> FindAll();
		Task<List<PEntity>> FindAllPartial();
	}
}
