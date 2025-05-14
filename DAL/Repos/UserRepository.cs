using DAL.DB;
using DAL.Models;
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

        public void Add(User entity)
        {
            dbContext.Users.Add(entity);
            SaveChanges();
        }

        public void Delete(User entity)
        {
            dbContext.Users.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return dbContext.Users.Where(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public User? GetByEmail(string email)
        {
            return dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetById(int id)
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            dbContext.Users.Update(entity);
            SaveChanges();
        }
    }
}
