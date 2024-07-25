using API.DTOs.Appointment;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _context.Appointment.ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto> GetByIdAsync(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<(bool Succeeded, int? AppointmentId, string ErrorMessage)> CreateAsync(CreateAppointmentDto createDto)
        {
            var appointment = _mapper.Map<Appointment>(createDto);

            // Kiểm tra sự tồn tại của Customer
            if (createDto.CustomerId.HasValue)
            {
                var customerExists = await _context.Customer.AnyAsync(c => c.Id == createDto.CustomerId.Value);
                if (!customerExists)
                {
                    return (false, null, "Customer does not exist.");
                }
            }

            // Kiểm tra sự tồn tại của Employee
            if (createDto.EmployeeId.HasValue)
            {
                var employeeExists = await _context.Employee.AnyAsync(e => e.Id == createDto.EmployeeId.Value);
                if (!employeeExists)
                {
                    return (false, null, "Employee does not exist.");
                }
            }

            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            return (true, appointment.Id, null); // Trả về ID của cuộc hẹn vừa tạo
        }

        public async Task<(bool Succeeded, string ErrorMessage)> UpdateAsync(int id, UpdateAppointmentDto updateDto)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return (false, "Appointment not found.");
            }

            // Kiểm tra sự tồn tại của Customer
            if (updateDto.CustomerId.HasValue)
            {
                var customerExists = await _context.Customer.AnyAsync(c => c.Id == updateDto.CustomerId.Value);
                if (!customerExists)
                {
                    return (false, "Customer does not exist.");
                }
            }

            // Kiểm tra sự tồn tại của Employee
            if (updateDto.EmployeeId.HasValue)
            {
                var employeeExists = await _context.Employee.AnyAsync(e => e.Id == updateDto.EmployeeId.Value);
                if (!employeeExists)
                {
                    return (false, "Employee does not exist.");
                }
            }

            _mapper.Map(updateDto, appointment);
            _context.Appointment.Update(appointment);
            await _context.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> DeleteAsync(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return (false, "Appointment not found.");
            }

            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return (true, null);
        }
    }
}