using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace API.Server.DTOs.Account
{
    public class RegisterEmpDto
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

    }
}