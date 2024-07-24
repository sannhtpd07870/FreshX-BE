using API.Server.DTOs.RolesDTO;
using API.Server.Interfaces;
using API.Server.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Server.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Role.FindAsync(id);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> CreateRoleAsync(CreateRoleDto roleDto)
        {
            var role = new Role
            {
                RoleName = roleDto.RoleName
            };

            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> UpdateRoleAsync(int id, UpdateRoleDto roleDto)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return (false, "Role not found.");
            }

            role.RoleName = roleDto.RoleName;
            _context.Role.Update(role);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> DeleteRoleAsync(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return (false, "Role not found.");
            }

            _context.Role.Remove(role);
            await _context.SaveChangesAsync();

            return (true, null);
        }
    }
}
