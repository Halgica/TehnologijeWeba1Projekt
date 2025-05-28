using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        public Task<Promotion> GetByNameAsync(string name);
        public Task<Promotion> GetByCodeAsync(string code);
    }
}
