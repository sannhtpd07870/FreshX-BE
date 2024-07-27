using System.Text;
using Newtonsoft.Json;
using Quartz;

namespace API.EmailAotu
{
    public class CallApiJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CallApiJob(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var client = _httpClientFactory.CreateClient();

            // Dữ liệu JSON cần gửi
            var emailData = new
            {
                toEmail = "nhatnvpd07532@fpt.edu.vn",
                subject = "Bạn iu ơi đừng quên uống thuốc nhé.",
                body = @"
                Nguyễn Văn Nhật thân mến,

                Đây là lời nhắc lịch uống thuốc hàng ngày của bạn. Xin vui lòng uống thuốc theo chỉ dẫn của bác sĩ để đảm bảo sức khỏe của bạn luôn được theo dõi và điều trị tốt nhất.

                Thông tin lịch uống thuốc:
                - Thời gian: 17:30
                - Loại thuốc: Paracetamol
                - Liều lượng: 500mg

                Hãy đảm bảo rằng bạn uống thuốc đúng giờ và không bỏ lỡ liều nào. Nếu bạn có bất kỳ câu hỏi nào về thuốc hoặc lịch uống thuốc, xin vui lòng liên hệ với bác sĩ hoặc nhân viên y tế của bạn.

                Chúc bạn mau chóng hồi phục và luôn khỏe mạnh!

                Trân trọng,

                Phòng Khám Đa Khoa FreshX
            "
            };

            var json = JsonConvert.SerializeObject(emailData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://freshx-api.azurewebsites.net/api/SendMail/SendEmail", content);

            if (response.IsSuccessStatusCode)
            {
                // Log success
            }
            else
            {
                // Log failure
            }
        }
    }
}