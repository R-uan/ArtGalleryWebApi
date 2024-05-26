using ArtGallery.Models;
using ArtGallery.Interfaces;
using ArtGallery.DTO;

namespace ArtGallery.Services {
	public class ArtistService(IArtistRepository repository) : IArtistService {

		private readonly IArtistRepository _repository = repository;

		public async Task<List<Artist>> GetAll() {
			return await _repository.FindAll();
		}

		public async Task<List<PartialArtistDTO>> GetAllPartial() {
			return await _repository.FindAllPartial();
		}

		public async Task<Artist?> GetOneById(int id) {
			return await _repository.FindById(id);
		}

		public async Task<Artist> PostOne(ArtistDTO artist) {
			Artist mapping = new() {
				Name = artist.Name,
				Slug = artist.Slug,
				Country = artist.Country,
				Biography = artist.Biography,
				Movement = artist.Movement,
				Profession = artist.Profession,
				Date_of_birth = artist.Date_of_birth,
				Date_of_death = artist.Date_of_death,
			};
			return await _repository.SaveOne(mapping);
		}

		public async Task<Artist?> UpdateOne(int id, UpdateArtistDTO artist) {
			return await _repository.UpdateById(id, artist);
		}

		public async Task<bool?> DeleteOne(int id) {
			return await _repository.DeleteById(id);
		}

		public async Task<Artist?> GetOneBySlug(string slug) {
			return await _repository.FindBySlug(slug);
		}
	}
}