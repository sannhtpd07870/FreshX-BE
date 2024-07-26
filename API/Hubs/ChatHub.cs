namespace API.Hubs
{
    using API.DTOs.Chat;
    using API.Interfaces;
    using Microsoft.AspNetCore.SignalR;


    /// <summary>
    /// Hub cho việc xử lý tin nhắn chat.
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatHub(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        /// <summary>
        /// Gửi tin nhắn và lưu trữ vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="message">Thông tin tin nhắn chat.</param>
        public async Task SendMessage(CreateChatMessageDto message)
        {
            // Lưu trữ tin nhắn vào cơ sở dữ liệu
            var result = await _chatMessageService.CreateAsync(message);

            if (result.Succeeded)
            {
                // Phát tin nhắn đến tất cả các client kết nối
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                // Xử lý lỗi nếu cần thiết
                throw new HubException(result.ErrorMessage);
            }
        }
    }
}
