namespace API.DTOs.Feedback
{
    public class AddingFeedback
    {
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } // Ngày và giờ của feedback
        public string? Status { get; set; } // Trạng thái của feedback
        public int Rating { get; set; }
    }
}
