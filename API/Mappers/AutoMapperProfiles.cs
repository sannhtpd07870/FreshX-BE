using AutoMapper;
using API.Server.Models;
using API.DTOs.AccountEmp;
using API.Server.DTOs.Account;
using API.DTOs.Account;
using API.Models;
using API.DTOs.MedicalRecord;
using API.DTOs.Customer;
using API.DTOs.Note;

namespace API
{
    // Cấu hình AutoMapper cho các đối tượng chuyển đổi
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Chuyển đổi từ AccountEmp sang AccountEmpDto và ngược lại
            CreateMap<AccountEmp, AccountEmpDto>().ReverseMap();
            
            //MedicalRecord
            CreateMap<MedicalRecord,MedicalRecordDto>().ReverseMap();
            CreateMap<MedicalRecord,AddingMedicalRecord>().ReverseMap();
            CreateMap<MedicalRecord,UpdatingMedicalRecord>().ReverseMap();
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Note,NoteDto>().ReverseMap(); 

            // Chuyển đổi từ RegisterEmpDto sang AccountEmp và khởi tạo Employee với giá trị null
            CreateMap<RegisterEmpDto, AccountEmp>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => new Employee
                {
                    Name = null,
                    BirthDate = default,
                    Gender = null,
                    Address = null,
                    Phone = null,
                    Email = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                }))
                .ForMember(dest => dest.RoleId, opt => opt.Ignore()) // Bỏ qua RoleId để đặt mặc định
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()); // Bỏ qua EmployeeId để đặt từ Employee mới tạo
        }
    }
}
