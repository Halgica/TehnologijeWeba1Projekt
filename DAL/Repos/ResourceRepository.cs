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
    public class ResourceRepository : IResourceRepository
    {
        private readonly TW1DbContext dbContext;

        public ResourceRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Resource resource)
        {
            await dbContext.Resources.AddAsync(resource);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Resource resource)
        {
            dbContext.Resources.Update(resource);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Resource resource)
        {
            dbContext.Resources.Remove(resource);
            await SaveChangesAsync();
        }

        public async Task<Resource?> GetByIdAsync(int id)
        {
            return await dbContext.Resources.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Resource?> GetByNameAsync(string name)
        {
            return await dbContext.Resources.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return await dbContext.Resources.ToListAsync();
        }

        public async Task<IEnumerable<Resource>> FindAsync(Expression<Func<Resource, bool>> predicate)
        {
            return await dbContext.Resources.Where(predicate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
