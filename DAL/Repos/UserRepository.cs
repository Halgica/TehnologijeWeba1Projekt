using DAL.DB;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly TW1DbContext dbContext;

        public UserRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            dbContext.Users.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await dbContext.Users.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            dbContext.Users.Update(entity);
            await SaveChangesAsync();
        }
    }
}
