using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Interface
{
    public interface IReviewRepository : IRepository<Review>
    {
        public Task<IEnumerable<Review>> GetByRatingAsync(int rating);
        public Task<IEnumerable<Review>> GetByEscapeRoomAsync(string escapeRoom);
    }
}
