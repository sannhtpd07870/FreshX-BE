using API.DTOs.MedicalRecord;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MedicalRecordService (ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MedicalRecord> CreatingMedicalRecordAsync(AddingMedicalRecord addingMedicalRecord)
        {
            var medicalRecordModel = _mapper.Map<MedicalRecord>(addingMedicalRecord);
            await _context.MedicalRecord.AddAsync(medicalRecordModel);
            _context.SaveChanges();
            return medicalRecordModel;
        }

        public async Task<MedicalRecord?> DeleteMedicalRecordByIdAsync(int medicalId)
        {
            var medicalRecord = await _context.MedicalRecord.FirstOrDefaultAsync(o => o.Id == medicalId);
            if (medicalRecord == null)
            {
                return null;
            }
            _context.MedicalRecord.Remove(medicalRecord);
            _context.SaveChanges();
            return medicalRecord;
        }

        public async Task<List<MedicalRecord>> GetAllMedicalRecordsAsync()
          {
            return await _context.MedicalRecord.Include(mr => mr.Customer).Include(mr => mr.Notes).ToListAsync();
        }

        public async Task<MedicalRecord?> GetMedicalRecordByIdAsync(int MedicalId)
        {
            var medicalRecord = await _context.MedicalRecord.Include(mr => mr.Customer).Include(mr => mr.Notes).FirstOrDefaultAsync(o => o.Id == MedicalId);
            return medicalRecord;
        }

        public async Task<MedicalRecord?> UpdateMedicalRecordByIdAsync(int MedicalId, UpdatingMedicalRecord updatingWalkDto)
        {
            var medicalRecord = await _context.MedicalRecord.FirstOrDefaultAsync(o => o.Id ==  MedicalId);
            if(medicalRecord == null) { return null;}
            medicalRecord.RecordDate = updatingWalkDto.RecordDate;
            medicalRecord.Details = updatingWalkDto.Details;
            await _context.SaveChangesAsync();
            medicalRecord = await _context.MedicalRecord.Include(mr => mr.Customer).Include(mr => mr.Notes).FirstOrDefaultAsync(o => o.Id == MedicalId);
            return medicalRecord;
        }
    }
}
