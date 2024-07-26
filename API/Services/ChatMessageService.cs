using API.DTOs.Chat;
using API.Hubs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    /// <summary>
    /// Service implement các phương thức từ IChatMessageService
    /// </summary>
    public class ChatMessageService : IChatMessageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _objectMapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatMessageService(ApplicationDbContext dbContext, IMapper objectMapper, IHubContext<ChatHub> hubContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        /// <summary>
        /// Lấy tất cả các tin nhắn chat
        /// </summary>
        public async Task<IEnumerable<ChatMessageDto>> GetAllAsync()
        {
            var chatMessages = await _dbContext.ChatMessages.Include(cm => cm.ChatSession).ToListAsync();
            return _objectMapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        /// <summary>
        /// Lấy tin nhắn chat theo mã khách hàng
        /// </summary>
        public async Task<IEnumerable<ChatMessageDto>> GetByCustomerIdAsync(int customerId)
        {
            var chatMessages = await _dbContext.ChatMessages
                .Include(cm => cm.ChatSession)
                .Where(cm => cm.ChatSession.CustomerId == customerId)
                .ToListAsync();
            return _objectMapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        /// <summary>
        /// Lấy tin nhắn chat theo mã nhân viên
        /// </summary>
        public async Task<IEnumerable<ChatMessageDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var chatMessages = await _dbContext.ChatMessages
                .Include(cm => cm.ChatSession)
                .Where(cm => cm.ChatSession.EmployeeId == employeeId)
                .ToListAsync();
            return _objectMapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        /// <summary>
        /// Lấy tin nhắn chat theo mã tin nhắn
        /// </summary>
        public async Task<ChatMessageDto> GetByIdAsync(int id)
        {
            var chatMessage = await _dbContext.ChatMessages
                .Include(cm => cm.ChatSession)
                .SingleOrDefaultAsync(cm => cm.Id == id);
            return _objectMapper.Map<ChatMessageDto>(chatMessage);
        }

        /// <summary>
        /// Tạo tin nhắn chat mới
        /// </summary>
        public async Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatMessageDto createDto)
        {
            var chatMessage = _objectMapper.Map<ChatMessage>(createDto);

            var chatSession = await _dbContext.ChatSessions.FindAsync(createDto.ChatSessionId);
            if (chatSession == null)
            {
                return (false, "Chat session not found.");
            }

            _dbContext.ChatMessages.Add(chatMessage);
            await _dbContext.SaveChangesAsync();

            // Phát tin nhắn đến các client qua SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", _objectMapper.Map<ChatMessageDto>(chatMessage));

            return (true, null);
        }

        /// <summary>
        /// Xóa tin nhắn chat theo mã tin nhắn
        /// </summary>
        public async Task<(bool Succeeded, string ErrorMessage)> DeleteAsync(int id)
        {
            var chatMessage = await _dbContext.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return (false, "Chat message not found.");
            }

            _dbContext.ChatMessages.Remove(chatMessage);
            await _dbContext.SaveChangesAsync();
            return (true, null);
        }
    }
}
