using DAL.DB;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly TW1DbContext _dbContext;

        public UserRepository(TW1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _dbContext.Users.Where(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _dbContext.Users.Update(entity);
            SaveChanges();
        }
    }
}
