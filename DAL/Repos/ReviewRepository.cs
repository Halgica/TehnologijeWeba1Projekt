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
    public class ReviewRepository : IReviewRepository
    {
        private readonly TW1DbContext dbContext;

        public ReviewRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Review entity)
        {
            await dbContext.Reviews.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Review entity)
        {
            dbContext.Reviews.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> FindAsync(Expression<Func<Review, bool>> predicate)
        {
            return await dbContext.Reviews.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await dbContext.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetByEscapeRoomAsync(string escapeRoom)
        {
            return await dbContext.Reviews
                .Where(r => r.EscapeRoom.Name == escapeRoom)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetByRatingAsync(int rating)
        {
            return await dbContext.Reviews.Where(r => r.Rating == rating).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review entity)
        {
            dbContext.Reviews.Update(entity);
            await SaveChangesAsync();
        }
    }
}
