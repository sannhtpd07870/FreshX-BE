namespace API.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatMessageService(ApplicationDbContext context, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<ChatMessageDto>> GetAllAsync()
        {
            var chatMessages = await _context.ChatMessages.Include(cm => cm.ChatSession).ToListAsync();
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        public async Task<IEnumerable<ChatMessageDto>> GetByCustomerIdAsync(int customerId)
        {
            var chatMessages = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .Where(cm => cm.ChatSession.CustomerId == customerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        public async Task<IEnumerable<ChatMessageDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var chatMessages = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .Where(cm => cm.ChatSession.EmployeeId == employeeId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        public async Task<ChatMessageDto> GetByIdAsync(int id)
        {
            var chatMessage = await _context.ChatMessages
                .Include(cm => cm.ChatSession)
                .SingleOrDefaultAsync(cm => cm.Id == id);
            return _mapper.Map<ChatMessageDto>(chatMessage);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatMessageDto createDto)
        {
            var chatMessage = _mapper.Map<ChatMessage>(createDto);

            var chatSession = await _context.ChatSessions.FindAsync(createDto.ChatSessionId);
            if (chatSession == null)
            {
                return (false, "Chat session not found.");
            }

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Phát tin nhắn đến các client qua SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", _mapper.Map<ChatMessageDto>(chatMessage));

            return (true, null);
        }

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