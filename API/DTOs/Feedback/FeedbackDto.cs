using API.DTOs.AccountCus;

namespace API.DTOs.Feedback
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } // Ngày và giờ của feedback
        public string? Status { get; set; } // Trạng thái của feedback (Scheduled, Completed, Cancelled)
        public int Rating { get; set; }
        public AccountDto? Account { get; set; }
    }
}
