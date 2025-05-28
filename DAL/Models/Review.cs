using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int EscapeRoomId { get; set; }
        public int Rating { get; set; }
        public string? Content { get; set; }
        public DateTime ReviewTime { get; set; } = DateTime.Now;

        public required virtual Resource EscapeRoom { get; set; }
        public required virtual User User { get; set; }
    }
}
