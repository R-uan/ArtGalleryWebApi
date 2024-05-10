namespace ArtGallery {
	public interface IRepository<TEntity, UEntity, PEntity> {
		Task<TEntity> SaveOne(TEntity TEntity);
		Task<bool?> DeleteById(int id);
		Task<bool?> UpdateById(int id, UEntity patch);

		Task<TEntity?> FindById(int id);
		Task<TEntity?> FindBySlug(string slug);

		Task<List<TEntity>> FindAll();
		Task<List<PEntity>> FindAllPartial();
	}
}
