using API.DTOs.Chat;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    /// <summary>
    /// Service implement các phương thức từ IChatMessageService
    /// </summary>
    public class ChatMessageService : IChatMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChatMessageService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy tất cả các tin nhắn chat
        /// </summary>
        /// <returns>Danh sách các tin nhắn chat</returns>
        public async Task<IEnumerable<ChatMessageDto>> GetAllAsync()
        {
            var chatMessages = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        /// <summary>
        /// Lấy các tin nhắn chat theo mã khách hàng
        /// </summary>
        /// <param name="customerId">Mã khách hàng</param>
        /// <returns>Danh sách các tin nhắn chat của khách hàng</returns>
        public async Task<IEnumerable<ChatMessageDto>> GetByCustomerIdAsync(int customerId)
        {
            var chatMessages = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .Where(cm => cm.ChatSession.CustomerId == customerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        /// <summary>
        /// Lấy các tin nhắn chat theo mã nhân viên
        /// </summary>
        /// <param name="employeeId">Mã nhân viên</param>
        /// <returns>Danh sách các tin nhắn chat của nhân viên</returns>
        public async Task<IEnumerable<ChatMessageDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var chatMessages = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .Where(cm => cm.ChatSession.EmployeeId == employeeId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        /// <summary>
        /// Lấy tin nhắn chat theo mã tin nhắn
        /// </summary>
        /// <param name="id">Mã tin nhắn</param>
        /// <returns>Tin nhắn chat</returns>
        public async Task<ChatMessageDto> GetByIdAsync(int id)
        {
            var chatMessage = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .SingleOrDefaultAsync(cm => cm.Id == id);
            return _mapper.Map<ChatMessageDto>(chatMessage);
        }

        /// <summary>
        /// Tạo mới tin nhắn chat
        /// </summary>
        /// <param name="createDto">Thông tin tạo tin nhắn chat</param>
        /// <returns>Kết quả tạo tin nhắn chat</returns>
        public async Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatMessageDto createDto)
        {
            var chatMessage = _mapper.Map<ChatMessage>(createDto);

            // Kiểm tra sự tồn tại của ChatSession
            var chatSession = await _context.ChatSessions.FindAsync(createDto.ChatSessionId);
            if (chatSession == null)
            {
                return (false, "Chat session not found.");
            }

            // Kiểm tra sự tồn tại của Customer nếu có
            if (createDto.CustomerId.HasValue)
            {
                var customerExists = await _context.Customer.AnyAsync(c => c.Id == createDto.CustomerId.Value);
                if (!customerExists)
                {
                    return (false, "Customer not found.");
                }
            }

            // Kiểm tra sự tồn tại của Employee nếu có
            if (createDto.EmployeeId.HasValue)
            {
                var employeeExists = await _context.Employee.AnyAsync(e => e.Id == createDto.EmployeeId.Value);
                if (!employeeExists)
                {
                    return (false, "Employee not found.");
                }
            }

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
            return (true, null);
        }

        /// <summary>
        /// Xóa tin nhắn chat
        /// </summary>
        /// <param name="id">Mã tin nhắn</param>
        /// <returns>Kết quả xóa tin nhắn chat</returns>
        public async Task<(bool Succeeded, string ErrorMessage)> DeleteAsync(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return (false, "Chat message not found.");
            }

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();
            return (true, null);
        }
    }
}
