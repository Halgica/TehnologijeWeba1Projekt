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
    public class PromotionRepository : IPromotionRepository
    {
        private readonly TW1DbContext dbContext;
        public PromotionRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Promotion entity)
        {
            dbContext.Add(entity);
            SaveChanges();
        }

        public void Delete(Promotion entity)
        {
            dbContext.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<Promotion> Find(Expression<Func<Promotion, bool>> predicate)
        {
            return dbContext.Promotions.Where(predicate);
        }

        public IEnumerable<Promotion> GetAll()
        {
            return dbContext.Promotions.ToList();
        }

        public Promotion GetByCode(string code)
        {
            return dbContext.Promotions.FirstOrDefault(p => p.Code == code);
        }

        public Promotion? GetById(int id)
        {
            return dbContext.Promotions.FirstOrDefault(p =>p.Id == id);
        }

        public Promotion GetByName(string name)
        {
            return dbContext.Promotions.FirstOrDefault(p => p.Name == name);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Promotion entity)
        {
            dbContext.Update(entity);
            SaveChanges();
        }
    }
}
