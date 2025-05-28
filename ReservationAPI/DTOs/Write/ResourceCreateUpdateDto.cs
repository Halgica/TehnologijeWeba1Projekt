using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.DTOs.Write
{
    public class ResourceCreateUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
