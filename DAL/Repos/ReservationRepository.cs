using DAL.DB;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task AddAsync(Reservation reservation)
        {
            await dbContext.Reservations.AddAsync(reservation);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            dbContext.Reservations.Update(reservation);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            dbContext.Reservations.Remove(reservation);
            await SaveChangesAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await dbContext.Reservations.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> FindAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return await dbContext.Reservations.Where(predicate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId)
        {
            return await dbContext.Reservations.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByResourceIdAsync(int resourceId)
        {
            return await dbContext.Reservations.Where(r => r.ResourceId == resourceId).ToListAsync();
        }
    }
}
