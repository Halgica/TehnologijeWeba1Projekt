using DAL.DB;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly TW1DbContext dbContext;

        public TimeSlotRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(TimeSlot entity)
        {
            await dbContext.TimeSlots.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TimeSlot entity)
        {
            dbContext.TimeSlots.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TimeSlot>> FindAsync(Expression<Func<TimeSlot, bool>> predicate)
        {
            return await dbContext.TimeSlots.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TimeSlot>> GetAllAsync()
        {
            return await dbContext.TimeSlots.ToListAsync();
        }

        public async Task<TimeSlot?> GetByIdAsync(int id)
        {
            return await dbContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeSlot entity)
        {
            dbContext.TimeSlots.Update(entity);
            await SaveChangesAsync();
        }
    }
}
