using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery {
	public class GalleryDbContext : DbContext {
		public GalleryDbContext(DbContextOptions<GalleryDbContext> options) : base(options) { }
		public DbSet<Admin> Admin { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Museum> Museums { get; set; }
		public DbSet<Artwork> Artworks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Artist>().ToTable("Artists");
			modelBuilder.Entity<Museum>().ToTable("Museums");
			modelBuilder.Entity<Artwork>().ToTable("Artworks");
			modelBuilder.Entity<Admin>().ToTable("Admins");

			modelBuilder.Entity<Artist>()
			.HasMany(artworks => artworks.Artworks)
			.WithOne(artist => artist.Artist)
			.HasForeignKey(key => key.ArtistId)
			.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Museum>()
			.HasMany(artwork => artwork.Artworks)
			.WithOne(museum => museum.Museum)
			.HasForeignKey(key => key.MuseumId)
			.OnDelete(DeleteBehavior.SetNull);
		}
	}
}