using System.ComponentModel.DataAnnotations;

namespace API.Server.DTOs.RolesDTO
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
