using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public PaymentType Type { get; set; }
        public virtual User? User { get; set; }
    }
    public enum PaymentType
    {
        Cash,
        Card,
        ApplePay,
        GooglePay,
        PayPal
    }
}
