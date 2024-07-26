namespace API.Models
{
    /// <summary>
    /// Model lưu trữ thông tin cuộc trò chuyện
    /// </summary>
    public class ChatSession
    {
        public int Id { get; set; } // Mã định danh của cuộc trò chuyện
        public int? CustomerId { get; set; } // Mã định danh của khách hàng (nếu có)
        public Customer Customer { get; set; } // Tham chiếu đến đối tượng khách hàng
        public int? EmployeeId { get; set; } // Mã định danh của nhân viên (nếu có)
        public Employee Employee { get; set; } // Tham chiếu đến đối tượng nhân viên
        public DateTime StartTime { get; set; } // Thời gian bắt đầu cuộc trò chuyện
        public DateTime? EndTime { get; set; } // Thời gian kết thúc cuộc trò chuyện (nếu có)
        public ICollection<ChatMessage> ChatMessages { get; set; } // Danh sách tin nhắn trong cuộc trò chuyện
    }
}