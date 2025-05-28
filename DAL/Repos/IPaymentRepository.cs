using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Payment>> GetByPaymentTypeAsync(PaymentType paymentType);

    }
}
