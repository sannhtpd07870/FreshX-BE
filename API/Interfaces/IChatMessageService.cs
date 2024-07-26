using API.DTOs.Chat;

namespace API.Interfaces
{
    /// <summary>
    /// Interface định nghĩa các phương thức cho dịch vụ ChatMessage
    /// </summary>
    public interface IChatMessageService
    {
        Task<IEnumerable<ChatMessageDto>> GetAllAsync(); // Lấy tất cả đoạn chat
        Task<IEnumerable<ChatMessageDto>> GetByCustomerIdAsync(int customerId); // Lấy đoạn chat theo CustomerId
        Task<IEnumerable<ChatMessageDto>> GetByEmployeeIdAsync(int employeeId); // Lấy đoạn chat theo EmployeeId
        Task<ChatMessageDto> GetByIdAsync(int id); // Lấy đoạn chat theo Id
        Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CreateChatMessageDto createDto); // Tạo đoạn chat mới
        Task<(bool Succeeded, string ErrorMessage)> DeleteAsync(int id); // Xóa đoạn chat theo Id
    }
}