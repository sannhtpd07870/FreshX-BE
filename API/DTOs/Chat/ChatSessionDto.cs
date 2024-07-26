using API.Models;

namespace API.DTOs.Chat
{
    /// <summary>
    /// DTO để trả về thông tin cuộc trò chuyện
    /// </summary>
    public class ChatSessionDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<ChatMessageDto> ChatMessages { get; set; } // Danh sách tin nhắn trong cuộc trò chuyện
    }
}