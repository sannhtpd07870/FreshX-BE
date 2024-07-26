using API.DTOs.Customer;
using API.DTOs.MedicalRecord;
using API.DTOs.Note;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IMapper _mapper;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService, IMapper mapper)
        {
            this._medicalRecordService = medicalRecordService;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecords()
        {
            var medicalRecords = await _medicalRecordService.GetAllMedicalRecordsAsync();
            return Ok(_mapper.Map<List<MedicalRecordDto>>(medicalRecords));
        }
        [HttpGet]
        [Route("{medicalRecordId:int}")]
        public async Task<IActionResult> GetMedicalRecordById([FromRoute] int medicalRecordId)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(medicalRecordId);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            var medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);
            return Ok(medicalRecordDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreatingNewMedicalRecord([FromBody] AddingMedicalRecord addingMedicalRecord)
        {
            if (ModelState.IsValid)
            {
                var medicalRecord = await _medicalRecordService.CreatingMedicalRecordAsync(addingMedicalRecord);
                var medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);

               return CreatedAtAction(nameof(GetMedicalRecordById), new { medicalRecordId = medicalRecord.Id }, medicalRecordDto);   
              
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("{medicalRecordId:int}")]
        public async Task<IActionResult> UpdatingMedicalRecord([FromRoute] int medicalRecordId, [FromBody] UpdatingMedicalRecord updatingMedicalRecord)
        {
            var medicalRecord = await _medicalRecordService.UpdateMedicalRecordByIdAsync(medicalRecordId, updatingMedicalRecord);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            var medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);
            return Ok(medicalRecord);
        }
        [HttpDelete]
        [Route("{medicalRecordId:int}")]
        public async Task<IActionResult> DeleteMedicalRecord([FromRoute] int medicalRecordId)
        {
            var medicalRecord = await _medicalRecordService.DeleteMedicalRecordByIdAsync(medicalRecordId);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
