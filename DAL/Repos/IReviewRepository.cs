using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IReviewRepository : IRepository<Review>
    {
        public IEnumerable<Review> GetByRating(int rating);
        public IEnumerable<Review> GetByEscapeRoom(string escapeRoom);
    }
}
