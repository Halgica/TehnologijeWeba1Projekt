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
        IEnumerable<Payment> GetByUserId(int userId);
        IEnumerable<Payment> GetByPaymentType(PaymentType paymentType);

    }
}
