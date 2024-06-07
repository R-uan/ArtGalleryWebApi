using ArtGallery.Models;
using ArtGallery.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery;

public class PeriodRepository(GalleryDbContext db) : IPeriodRepository {
	private readonly GalleryDbContext _db = db;

	public async Task<bool?> DeletePeriod(int id) {
		var target = await _db.Periods.FindAsync(id);
		if (target == null) return null;
		_db.Periods.Remove(target);
		await _db.SaveChangesAsync();
		return await _db.Periods.FindAsync(id) == null;
	}

	public async Task<Period?> FindOnePeriod(int id) {
		var period = await _db.Periods.FindAsync(id);
		if (period == null) return null;
		return period;
	}

	public async Task<List<PartialPeriod>> FindPartialPeriods() {
		var periods = from period in _db.Periods
									select new PartialPeriod {
										PeriodId = period.PeriodId,
										Name = period.Name
									};
		return await periods.ToListAsync();
	}

	public async Task<List<Period>> FindPeriods() {
		return await _db.Periods.ToListAsync();
	}

	public async Task<Period?> SavePeriod(Period period) {
		var save = await _db.Periods.AddAsync(period);
		await _db.SaveChangesAsync();
		return save.Entity;
	}

}
