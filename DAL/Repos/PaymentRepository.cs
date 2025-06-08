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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TW1DbContext dbContext;

        public PaymentRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Payment entity)
        {
            await dbContext.Payments.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Payment entity)
        {
            dbContext.Payments.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> FindAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await dbContext.Payments.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await dbContext.Payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await dbContext.Payments.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetByPaymentTypeAsync(PaymentType paymentType)
        {
            return await dbContext.Payments.Where(p => p.Type == paymentType).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByUserIdAsync(int userId)
        {
            return await dbContext.Payments.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            dbContext.Payments.Update(entity);
            await SaveChangesAsync();
        }
    }
}
