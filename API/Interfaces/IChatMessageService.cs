namespace API.Interfaces
{
    /// <summary>
    /// Interface cho dịch vụ xử lý tin nhắn chat
    /// </summary>
    public interface IChatMessageService
    {
        Task<IEnumerable<ChatMessageDto>> GetAllAsync();
        Task<IEnumerable<ChatMessageDto>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<ChatMessageDto>> GetByEmployeeIdAsync(int employeeId);
        Task<ChatMessageDto> GetByIdAsync(int id);
        Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatMessageDto createDto);
        Task<(bool Succeeded, string ErrorMessage)> DeleteAsync(int id);
    }
}