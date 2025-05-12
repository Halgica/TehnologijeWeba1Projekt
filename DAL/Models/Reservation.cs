using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResourceId { get; set; }
        public int SlotId { get; set; }

        public required virtual TimeSlot Slot { get; set; }
        public virtual User? User { get; set; }
        public virtual Resource? Resource { get; set; }
       
    }
}
