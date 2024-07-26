using API.Server.DTOs.RolesDTO;
using API.Server.Interfaces;
using API.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // API GET tất cả vai trò
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        // API GET vai trò theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // API POST tạo vai trò mới
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errorMessage) = await _roleService.CreateRoleAsync(roleDto);
            if (!succeeded)
            {
                return BadRequest(errorMessage);
            }

            return Ok("Role created successfully.");
        }

        // API PUT cập nhật vai trò
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errorMessage) = await _roleService.UpdateRoleAsync(id, roleDto);
            if (!succeeded)
            {
                return BadRequest(errorMessage);
            }

            return Ok("Role updated successfully.");
        }

        // API DELETE xóa vai trò
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (succeeded, errorMessage) = await _roleService.DeleteRoleAsync(id);
            if (!succeeded)
            {
                return BadRequest(errorMessage);
            }

            return Ok("Role deleted successfully.");
        }
    }
}
