using API.DTOs.Customer;
using API.DTOs.Note;

namespace API.DTOs.MedicalRecord
{
    public class MedicalRecordDto
    {
        public int Id { get; set; } // Mã định danh của hồ sơ y tế
        public DateTime RecordDate { get; set; } // Ngày ghi nhận hồ sơ
        public string? Details { get; set; } // Chi tiết hồ sơ
        public CustomerDto? Customer { get; set; }
        public List<NoteDto> Notes { get; set; } = new List<NoteDto>();
    }
}
