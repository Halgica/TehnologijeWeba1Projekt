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
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly TW1DbContext dbContext;
        public TimeSlotRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TimeSlot entity)
        {
            dbContext.Add(entity);
            SaveChanges();
        }

        public void Delete(TimeSlot entity)
        {
            dbContext.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<TimeSlot> Find(Expression<Func<TimeSlot, bool>> predicate)
        {
            return dbContext.TimeSlots.Where(predicate);
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            return dbContext.TimeSlots.ToList();
        }

        public TimeSlot? GetById(int id)
        {
            return dbContext.TimeSlots.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(TimeSlot entity)
        {
            dbContext.Update(entity);
            SaveChanges();
        }
    }
}
