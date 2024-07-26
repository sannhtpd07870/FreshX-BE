namespace API.DTOs.Note
{
    public class NoteDto
    {
        public int Id { get; set; } // Mã định danh của ghi chú
        public string? Content { get; set; } // Nội dung của ghi chú
        public DateTime CreatedDate { get; set; } // Ngày tạo ghi chú
    }
}
