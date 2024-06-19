using ArtGallery.Utils;
namespace ArtGallery.Interfaces.Services
{
    public interface IBaseService<TEntity, TEntityDTO, UEntity, PEntity>
    {
        Task<TEntity> Save(TEntityDTO artist);
        Task<TEntity?> FindById(int id);
        Task<TEntity?> FindBySlug(string slug);
        Task<List<TEntity>> All();
        Task<List<PEntity>> Partial();
        Task<bool?> Delete(int id);
        Task<TEntity?> Update(int id, UEntity artist);
        Task<PaginatedResponse<PEntity>> PartialPaginated(int pageIndex);

    }
}
