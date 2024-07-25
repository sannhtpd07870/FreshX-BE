using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountEmp
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}