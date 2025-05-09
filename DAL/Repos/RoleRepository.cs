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
    public class RoleRepository : IRoleRepository
    {
        private readonly TW1DbContext dbContext;

        public RoleRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Role entity)
        {
            dbContext.Roles.Add(entity);
            SaveChanges();
        }

        public void Delete(Role entity)
        {
            dbContext.Roles.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<Role> Find(Expression<Func<Role, bool>> predicate)
        {
            return dbContext.Roles.Where(predicate);
        }

        public IEnumerable<Role> GetAll()
        {
            return dbContext.Roles.ToList();
        }

        public Role? GetById(int id)
        {
            return dbContext.Roles.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Role> GetByName(RoleName name)
        {
            return dbContext.Roles.Where(r => r.Name == name);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Role entity)
        {
            dbContext.Roles.Update(entity);
            SaveChanges();
        }
    }
}
