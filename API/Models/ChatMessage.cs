namespace API.Models
{
    /// <summary>
    /// Model lưu trữ thông tin đoạn chat
    /// </summary>
    public class ChatMessage
    {
        public int Id { get; set; } // Mã định danh của đoạn chat
        public int? ChatSessionId { get; set; } // Mã định danh của cuộc trò chuyện
        public ChatSession ChatSession { get; set; } // Tham chiếu đến đối tượng cuộc trò chuyện
        public string Sender { get; set; } // Người gửi (Customer, Employee, hoặc Bot)
        public string Message { get; set; } // Nội dung đoạn chat
        public string MessageType { get; set; } // Loại tin nhắn (text, image, etc.)
        public DateTime Timestamp { get; set; } // Thời gian gửi
        public string ImageUrl { get; set; } // URL của hình ảnh (nếu có)
    }
}