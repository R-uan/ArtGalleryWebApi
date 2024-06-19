using ArtGallery.DTO;
using ArtGallery.Models;
namespace ArtGallery.Interfaces.Repository
{
    public interface IMuseumRepository : IBaseRepository<Museum, UpdateMuseumDTO, PartialMuseumDTO, MuseumQueryParams>
    {

    }
}

