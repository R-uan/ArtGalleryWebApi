using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery;

public class ArtistSeeder(ModelBuilder modelBuilder) {
	private readonly ModelBuilder _modelBuilder = modelBuilder;
	public void Seed() {
		this._modelBuilder.Entity<Artist>(period => {
			period.HasData(
				new Artist() {
					ArtistId = 1,
					Name = "Leonardo Da Vinci",
					Country = "Italy",
					Slug = "leonardo-da-vinci",
					ImageURL = "https://p2.trrsf.com/image/fget/cf/774/0/images.terra.com/2019/06/24/leo.jpg",
					Profession = "Polymath",
					Movement = "Renaissence",
				}
			);
		});
	}
}
