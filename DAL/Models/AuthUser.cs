using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)] // Swagger zna da je ovo pass
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public virtual AuthRole Role { get; set; } = null!; // Dopusti Null
        public virtual List<Reservation>? Reservations { get; set; }
    }
}
