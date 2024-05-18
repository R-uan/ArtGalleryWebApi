namespace ArtGallery.Interfaces {
    public interface IAdminService {
        public Task<string?> Authenticate(string username, string password);
    }
}