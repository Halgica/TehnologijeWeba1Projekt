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
    public class ReviewRepository : IReviewRepository
    {
        private readonly TW1DbContext dbContext;
        public ReviewRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Review entity)
        {
            dbContext.Add(entity);
        }

        public void Delete(Review entity)
        {
            dbContext.Remove(entity);
        }

        public IEnumerable<Review> Find(Expression<Func<Review, bool>> predicate)
        {
            return dbContext.Reviews.Where(predicate);
        }

        public IEnumerable<Review> GetAll()
        {
            return dbContext.Reviews.ToList();
        }

        public IEnumerable<Review> GetByEscapeRoom(string escapeRoom)
        {
            return dbContext.Reviews.Where(r => r.EscapeRoom.Name == escapeRoom);
        }

        public Review? GetById(int id)
        {
            return dbContext.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Review> GetByRating(int rating)
        {
            return dbContext.Reviews.Where(r => r.Rating == rating);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Review entity)
        {
            dbContext.Update(entity);
        }
    }
}
