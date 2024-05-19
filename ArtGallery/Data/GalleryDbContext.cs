using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery {
	public class GalleryDbContext(DbContextOptions<GalleryDbContext> options) : DbContext(options) {
        public DbSet<Admin> Admin { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Museum> Museums { get; set; }
		public DbSet<Artwork> Artworks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Artist>(entity => {
				entity.ToTable("Artists");
				entity.Property(column => column.Biography).HasColumnType("TEXT"); 
			});
			
			modelBuilder.Entity<Artwork>(entity => {
				entity.ToTable("Artworks");
				entity.Property(column => column.History).HasColumnType("TEXT");
			});
			
			modelBuilder.Entity<Museum>().ToTable("Museums");
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