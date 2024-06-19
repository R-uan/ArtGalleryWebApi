using ArtGallery.Utils;
using ArtGallery.Models;
using ArtGallery.Interfaces.Services;
using ArtGallery.Interfaces.Repository;

namespace ArtGallery.Application.Services {
	public class AdminService(IAdminRepository repository, JWTHelper authHelper) : IAdminService {
		private readonly JWTHelper _authHelper = authHelper;
		private readonly IAdminRepository _repository = repository;

		public async Task<string?> Authenticate(string username, string password) {
			Admin? admin = await _repository.FindByUsername(username);
			if (admin == null) return null;
			bool valid = BC.Verify(password, admin.Password);
			if (!valid) return null;
			return _authHelper.Generate(admin);
		}

		public async Task<int?> Register(string username, string password) {
			string hashed_password = BC.HashPassword(password);
			Admin admin = new() { Username = username, Password = hashed_password };
			return await _repository.AddOneAdmin(admin);
		}
	}
}