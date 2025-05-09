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
        public Promotion GetByName(string name);
        public Promotion GetByCode(string code);
    }
}
