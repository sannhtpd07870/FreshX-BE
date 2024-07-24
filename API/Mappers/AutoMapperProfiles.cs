using AutoMapper;
using API.Server.Models;
using API.DTOs.Account;
using API.Server.DTOs.RolesDTO;

namespace API
{
    // Cấu hình AutoMapper cho các đối tượng chuyển đổi
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AccountEmp, AccountEmpDto>(); // Chuyển đổi từ AccountEmp sang AccountEmpDto
            CreateMap<CreateAccountEmpDto, AccountEmp>(); // Chuyển đổi từ CreateAccountEmpDto sang AccountEmp
            CreateMap<UpdateAccountEmpDto, AccountEmp>();


            CreateMap<Role, RoleDto>(); // Chuyển đổi từ Role sang RoleDto
            CreateMap<CreateRoleDto, Role>(); // Chuyển đổi từ CreateRoleDto sang Role
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
