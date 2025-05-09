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
    public class ReservationRepository : IReservationRepository
    {
        private readonly TW1DbContext dbContext;

        public ReservationRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Reservation reservation)
        {
            dbContext.Reservations.Add(reservation);
            SaveChanges();
        }

        public void Update(Reservation reservation)
        {
            dbContext.Reservations.Update(reservation);
            SaveChanges();
        }

        public void Delete(Reservation reservation)
        {
            dbContext.Reservations.Remove(reservation);
            SaveChanges();
        }

        public Reservation? GetById(int id)
        {
            return dbContext.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return dbContext.Reservations.ToList();
        }

        public IEnumerable<Reservation> Find(Expression<Func<Reservation, bool>> predicate)
        {
            return dbContext.Reservations.Where(predicate);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public IEnumerable<Reservation> GetByUserId(int userId)
        {
            return dbContext.Reservations.Where(r => r.UserId == userId);
        }

        public IEnumerable<Reservation> GetByResourceId(int resourceId)
        {
            return dbContext.Reservations.Where(r => r.ResourceId == resourceId);
        }

        public IEnumerable<Reservation> GetBySlotId(int slotId)
        {
            return dbContext.Reservations.Where(r =>r.SlotId == slotId);
        }
    }
}
