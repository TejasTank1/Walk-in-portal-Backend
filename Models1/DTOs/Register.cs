using System.ComponentModel.DataAnnotations;

namespace Backend.Models1.DTOs
{
    public class Register
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
