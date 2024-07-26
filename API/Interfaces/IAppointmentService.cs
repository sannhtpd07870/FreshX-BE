using API.DTOs.Appointment;

namespace API.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto> GetByIdAsync(int id);
        Task<(bool Succeeded, int? AppointmentId, string ErrorMessage)> CreateAsync(CreateAppointmentDto createDto);
        Task<(bool Succeeded, string ErrorMessage)> UpdateAsync(int id, UpdateAppointmentDto updateDto);
        Task<(bool Succeeded, string ErrorMessage)> DeleteAsync(int id);
    }
}