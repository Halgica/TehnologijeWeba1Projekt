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
    public class RoleRepository : IRoleRepository
    {
        private readonly TW1DbContext dbContext;

        public RoleRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Role entity)
        {
            await dbContext.Roles.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Role entity)
        {
            dbContext.Roles.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            return await dbContext.Roles.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Role>> GetByNameAsync(RoleName name)
        {
            return await dbContext.Roles.Where(r => r.Name == name).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role entity)
        {
            dbContext.Roles.Update(entity);
            await SaveChangesAsync();
        }
    }
}
