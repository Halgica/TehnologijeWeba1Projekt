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
    public class ResourceRepository : IResourceRepository
    {
        private readonly TW1DbContext dbContext;

        public ResourceRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Resource resource)
        {
            dbContext.Resources.Add(resource);
            SaveChanges();
        }

        public void Update(Resource resource)
        {
            dbContext.Resources.Update(resource);
            SaveChanges();
        }

        public void Delete(Resource resource)
        {
            dbContext.Resources.Remove(resource);
            SaveChanges();
        }

        public Resource? GetById(int id)
        {
            return dbContext.Resources.FirstOrDefault(r => r.Id == id);
        }

        public Resource? GetByName(string name)
        {
            return dbContext.Resources.FirstOrDefault(r => r.Name == name);
        }

        public IEnumerable<Resource> GetAll()
        {
            return dbContext.Resources.ToList();
        }

        public IEnumerable<Resource> Find(Expression<Func<Resource, bool>> predicate)
        {
            return dbContext.Resources.Where(predicate);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
