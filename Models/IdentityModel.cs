using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class IdentityModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}