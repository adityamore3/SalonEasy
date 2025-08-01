using System.ComponentModel.DataAnnotations;

namespace SalonEasy.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
