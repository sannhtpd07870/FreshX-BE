using System.ComponentModel.DataAnnotations;

namespace API.Server.DTOs.Account
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}