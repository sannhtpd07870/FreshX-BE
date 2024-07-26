namespace API.DTOs.Chat
{
    /// <summary>
    /// DTO để tạo mới tin nhắn chat
    /// </summary>
    public class CreateChatMessageDto
    {
        public int? ChatSessionId { get; set; } // Mã định danh của cuộc trò chuyện
        public int? CustomerId { get; set; } // Mã định danh của khách hàng (nếu có)
        public int? EmployeeId { get; set; } // Mã định danh của nhân viên (nếu có)
        public string Sender { get; set; } // Người gửi (Customer, Employee, hoặc Bot)
        public string Message { get; set; } // Nội dung tin nhắn
        public string MessageType { get; set; } // Loại tin nhắn (text, image, etc.)
        public DateTime Timestamp { get; set; } // Thời gian gửi
        public string ImageUrl { get; set; } // URL của hình ảnh (nếu có)
    }
}
