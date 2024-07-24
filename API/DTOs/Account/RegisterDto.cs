using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace API.Server.DTOs.Account
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}