using API.DTOs.Chat;
using API.Models;
using API.Server.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Server.Services
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChatSessionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChatSessionDto>> GetAllAsync()
        {
            var chatSessions = await _context.ChatSessions.Include(cs => cs.ChatMessages).ToListAsync();
            return _mapper.Map<IEnumerable<ChatSessionDto>>(chatSessions);
        }

        public async Task<ChatSessionDto> GetByIdAsync(int id)
        {
            var chatSession = await _context.ChatSessions.Include(cs => cs.ChatMessages)
                .SingleOrDefaultAsync(cs => cs.Id == id);
            return _mapper.Map<ChatSessionDto>(chatSession);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatSessionDto createDto)
        {
            var chatSession = _mapper.Map<ChatSession>(createDto);
            _context.ChatSessions.Add(chatSession);
            await _context.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> EndSessionAsync(int id)
        {
            var chatSession = await _context.ChatSessions.FindAsync(id);
            if (chatSession == null)
            {
                return (false, "Chat session not found.");
            }

            chatSession.EndTime = DateTime.UtcNow;
            _context.ChatSessions.Update(chatSession);
            await _context.SaveChangesAsync();
            return (true, null);
        }
    }
}
