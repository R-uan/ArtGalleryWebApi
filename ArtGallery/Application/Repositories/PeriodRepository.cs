using ArtGallery.Models;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Interfaces.Repository;
using ArtGallery.Data.DataTransferObjects.Period;

namespace ArtGallery.Application.Repositories
{
    public class PeriodRepository(GalleryDbContext db) : IPeriodRepository
    {
        private readonly GalleryDbContext _db = db;

        public async Task<bool?> Delete(int id)
        {
            var target = await _db.Periods.FindAsync(id);
            if (target == null) return null;
            _db.Periods.Remove(target);
            await _db.SaveChangesAsync();
            return await _db.Periods.FindAsync(id) == null;
        }

        public async Task<Period?> FindById(int id)
        {
            var period = await _db.Periods.FindAsync(id);
            if (period == null) return null;
            return period;
        }

        public async Task<List<PartialPeriod>> FindPartial()
        {
            var periods = from period in _db.Periods
                          select new PartialPeriod
                          {
                              PeriodId = period.PeriodId,
                              Name = period.Name
                          };
            return await periods.ToListAsync();
        }

        public async Task<List<Period>> Find()
        {
            return await _db.Periods.ToListAsync();
        }

        public async Task<Period?> Save(Period period)
        {
            var save = await _db.Periods.AddAsync(period);
            await _db.SaveChangesAsync();
            return save.Entity;
        }

        public async Task<bool?> Update(int id, UpdatePeriod period)
        {
            var to_update = await _db.Periods.FindAsync(id);
            if (to_update == null) return null;
            if (period.Start != null) to_update.Start = period.Start.Value;
            if (period.End != null) to_update.End = period.End.Value;
            if(!string.IsNullOrEmpty(period.Name)) to_update.Name = period.Name;
            if(!string.IsNullOrEmpty(period.Summary)) to_update.Summary = period.Summary;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}