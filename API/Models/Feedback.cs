namespace API.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } // Ngày và giờ của feedback
        public string? Status { get; set; } // Trạng thái của feedback (Scheduled, Completed, Cancelled)
        public int Rating { get; set; }
        public int? AccountId { get; set; } // Id tài khoản của khách hàng (bệnh nhân)
        public AccountCus AccountCus { get; set; } // Tham chiếu đến đối tượng tài khoản khách hàng  
    }
}
