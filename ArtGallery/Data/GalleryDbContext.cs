using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
namespace ArtGallery;

public class GalleryDbContext(DbContextOptions<GalleryDbContext> options) : DbContext(options)
{
	public DbSet<Admin> Admin { get; set; }
	public DbSet<Artist> Artists { get; set; }
	public DbSet<Museum> Museums { get; set; }
	public DbSet<Period> Periods { get; set; }
	public DbSet<Artwork> Artworks { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Admin>().ToTable("Admins");
		modelBuilder.Entity<Artist>(artist =>
		{
			artist.ToTable("Artists");
			artist.Property(column => column.Biography).HasColumnType("TEXT");
			artist.HasMany(artist => artist.Artworks)
						.WithOne(artwork => artwork.Artist)
						.HasForeignKey(artwork => artwork.ArtistId)
						.OnDelete(DeleteBehavior.SetNull);
		});
		modelBuilder.Entity<Artwork>(artwork =>
		{
			artwork.ToTable("Artworks");
			artwork.Property(column => column.History).HasColumnType("TEXT");
		});
		modelBuilder.Entity<Museum>(museum =>
		{
			museum.ToTable("Museums");
			museum.HasMany(museum => museum.Artworks)
						.WithOne(artwork => artwork.Museum)
						.HasForeignKey(artwork => artwork.MuseumId)
						.OnDelete(DeleteBehavior.SetNull);
		});
		modelBuilder.Entity<Period>(period =>
		{
			period.ToTable("Periods");
			period.HasMany(period => period.Artworks)
						.WithOne(artwork => artwork.Period)
						.HasForeignKey(artwork => artwork.PeriodId)
						.OnDelete(DeleteBehavior.SetNull);
		});

	}
}
