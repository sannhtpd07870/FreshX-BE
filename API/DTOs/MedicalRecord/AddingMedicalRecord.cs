namespace API.DTOs.MedicalRecord
{
    public class AddingMedicalRecord
    {
        public DateTime RecordDate { get; set; } // Ngày ghi nhận hồ sơ
        public string? Details { get; set; } // Chi tiết hồ sơ
        public int? CustomerId { get; set; } // Mã khách hàng
    }
}
