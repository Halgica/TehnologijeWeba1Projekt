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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TW1DbContext dbContext;
        public PaymentRepository(TW1DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Payment entity)
        {
            dbContext.Add(entity);
            SaveChanges();
        }

        public void Delete(Payment entity)
        {
            dbContext.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<Payment> Find(Expression<Func<Payment, bool>> predicate)
        {
            return dbContext.Payments.Where(predicate);
        }

        public IEnumerable<Payment> GetAll()
        {
            return dbContext.Payments.ToList();
        }

        public Payment? GetById(int id)
        {
            return dbContext.Payments.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Payment> GetByPaymentType(PaymentType paymentType)
        {
            return dbContext.Payments.Where(p => p.Type == paymentType);
        }

        public IEnumerable<Payment> GetByUserId(int userId)
        {
            return dbContext.Payments.Where(p => p.UserId == userId);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Payment entity)
        {
            dbContext.Update(entity);
            SaveChanges();
        }
    }
}
