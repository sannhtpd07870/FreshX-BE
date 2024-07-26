using API.Models;
using API.DTOs.MedicalRecord;

namespace API.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<List<MedicalRecord>> GetAllMedicalRecordsAsync();
        Task<MedicalRecord?> GetMedicalRecordByIdAsync(int MedicalId);
        Task<MedicalRecord?> UpdateMedicalRecordByIdAsync(int MedicalId, UpdatingMedicalRecord updatingWalkDto);
        Task<MedicalRecord> CreatingMedicalRecordAsync(AddingMedicalRecord addingMedicalRecord);
        Task<MedicalRecord?> DeleteMedicalRecordByIdAsync(int medicalId);
    }
}
