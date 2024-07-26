namespace API.DTOs.Chat
{
    /// <summary>
    /// DTO để tạo cuộc trò chuyện mới
    /// </summary>
    public class CreateChatSessionDto
    {
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
    }
}