using API.Server.DTOs.RolesDTO;
using API.Server.Models;

namespace API.Server.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task<(bool Succeeded, string ErrorMessage)> CreateRoleAsync(CreateRoleDto roleDto);
        Task<(bool Succeeded, string ErrorMessage)> UpdateRoleAsync(int id, UpdateRoleDto roleDto);
        Task<(bool Succeeded, string ErrorMessage)> DeleteRoleAsync(int id);
    }
}
