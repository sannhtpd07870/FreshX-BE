namespace API.DTOs.Chat
{
    /// <summary>
    /// DTO để trả về thông tin đoạn chat
    /// </summary>
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ImageUrl { get; set; }
    }
}