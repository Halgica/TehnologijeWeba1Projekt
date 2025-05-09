using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public required RoleName Name { get; set; }
        public IList<User>? users { get; set; }
    }
    public enum RoleName
    {
        user,
        admin
    }
}
