namespace API.DTOs.MedicalRecord
{
    public class UpdatingMedicalRecord
    {
        public DateTime RecordDate { get; set; } // Ngày ghi nhận hồ sơ
        public string? Details { get; set; } // Chi tiết hồ sơ
    }
}
