namespace DAL.Models
{
    // Roles: 0 - User, 1 - Admin
    public class AuthRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public virtual ICollection<AuthUser> Users { get; set; } = new List<AuthUser>();
    }
}