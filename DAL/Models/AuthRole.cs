namespace DAL.Models
{
    public class AuthRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<AuthUser> Users { get; set; } = new List<AuthUser>();
    }
}