using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float DiscountAmount { get; set; }
        public DateTime PromotionEndTime { get; set; }
        public int UsageCount { get; set; }
    }
}
