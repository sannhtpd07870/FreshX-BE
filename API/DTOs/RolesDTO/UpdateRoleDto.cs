using System.ComponentModel.DataAnnotations;

namespace API.Server.DTOs.RolesDTO
{
    public class UpdateRoleDto
    {
        [Required]
        public string RoleName { get; set; }
    }
}
