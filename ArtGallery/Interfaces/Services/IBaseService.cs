namespace ArtGallery.Interfaces {
	public interface IBaseService<TEntity, TEntityDTO, UEntity, PEntity> {
		public Task<TEntity> PostOne(TEntityDTO artist);
		public Task<TEntity?> GetOneById(int id);
		public Task<TEntity?> GetOneBySlug(string slug);

		public Task<List<TEntity>> GetAll();
		public Task<List<PEntity>> GetAllPartial();

		public Task<bool?> DeleteOne(int id);
		public Task<TEntity?> UpdateOne(int id, UEntity artist);
	}
}