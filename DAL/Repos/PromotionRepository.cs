using DAL.DB;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly TW1DbContext dbContext;

        public PromotionRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Promotion entity)
        {
            await dbContext.Promotions.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Promotion entity)
        {
            dbContext.Promotions.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Promotion>> FindAsync(Expression<Func<Promotion, bool>> predicate)
        {
            return await dbContext.Promotions.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetAllAsync()
        {
            return await dbContext.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetByCodeAsync(string code)
        {
            return await dbContext.Promotions.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Promotion?> GetByIdAsync(int id)
        {
            return await dbContext.Promotions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Promotion?> GetByNameAsync(string name)
        {
            return await dbContext.Promotions.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Promotion entity)
        {
            dbContext.Promotions.Update(entity);
            await SaveChangesAsync();
        }
    }
}
