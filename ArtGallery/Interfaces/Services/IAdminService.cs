namespace ArtGallery.Interfaces.Services
{
    public interface IAdminService
    {
        Task<string?> Authenticate(string username, string password);
        Task<int?> Register(string username, string password);
    }
}
