using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountEmp
{
    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}