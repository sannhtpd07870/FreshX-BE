using API.DTOs.Appointment;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateAppointmentDto createDto)
        {
            var result = await _appointmentService.CreateAsync(createDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.AppointmentId }, result.AppointmentId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateAppointmentDto updateDto)
        {
            var result = await _appointmentService.UpdateAsync(id, updateDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _appointmentService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }
    }
}