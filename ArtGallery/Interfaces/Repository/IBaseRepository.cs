using ArtGallery.Utils;
namespace ArtGallery.Interfaces.Repository
{
    public interface IBaseRepository<TEntity, UEntity, PEntity, QEntity>
    {

        //
        //	Takes an TEntity and attempts to save it on the database.
        //
        Task<TEntity> Save(TEntity entity);
        // 
        // Takes an ID and tries to delete the record. 
        // If deleted, returns true, else false. 
        // If not found returns null 
        //
        Task<bool?> DeleteById(int id);
        // 
        // Takes an ID and a UEntity and tries to update the record and returns it
        // If ID not found, returns null.
        // If the patch is null throws an exception.
        //
        Task<TEntity?> UpdateById(int id, UEntity patch);
        //
        //	Takes an ID and returns the record associated to it.
        // If record is not found, returns null.
        //
        Task<TEntity?> FindById(int id);
        //
        // Takes an SLUG and returns the record associated to it.
        // If record is not found, returns null.
        //
        Task<TEntity?> FindBySlug(string slug);
        //
        // Returns a list of all records available in the database.
        // Entity contains all rows.
        // Always returns a List, even if it's empty.
        //
        Task<List<TEntity>> Find();
        //
        // Returns a list of all records available in the database.
        // Entity contains only a partial amount of data.
        // Always returns a List, even if it's empty.
        //
        Task<List<PEntity>> FindPartial();
        //
        // Returns a PaginatedResponse object with a list of PEntity 
        // and information about the pagination. 
        //
        Task<PaginatedResponse<PEntity>> FindPartialPaginated(int pageIndex);

        Task<PaginatedResponse<PEntity>> PaginatedQuery(QEntity queryParams, int pageIndex);

    }
}
