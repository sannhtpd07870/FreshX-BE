namespace API.Models
{
    public class MailRequest
    {
        public string FromEmail = "nguyenkinhthanh11@gmail.com";
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
