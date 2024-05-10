namespace ArtGallery {
	public interface IService<TEntity, UEntity, PEntity> {
		public Task<TEntity> PostOne(TEntity artist);
		public Task<TEntity?> GetOneById(int id);
		public Task<TEntity?> GetOneBySlug(string slug);

		public Task<List<TEntity>> GetAll();
		public Task<List<PEntity>> GetAllPartial();

		public Task<bool?> DeleteOne(int id);
		public Task<bool?> UpdateOne(int id, UEntity artist);
	}
}