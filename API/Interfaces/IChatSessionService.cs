using API.DTOs.Chat;
namespace API.Server.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các phương thức cho dịch vụ ChatSession
    /// </summary>
    public interface IChatSessionService
    {
        Task<IEnumerable<ChatSessionDto>> GetAllAsync();
        Task<ChatSessionDto> GetByIdAsync(int id);
        Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatSessionDto createDto);
        Task<(bool Succeeded, string ErrorMessage)> EndSessionAsync(int id);
    }
}
