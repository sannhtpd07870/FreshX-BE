using System.ComponentModel.DataAnnotations;

namespace API.Server.DTOs.Account
{
    public class LoginEmpDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}