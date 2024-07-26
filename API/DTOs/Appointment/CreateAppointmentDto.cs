namespace API.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}