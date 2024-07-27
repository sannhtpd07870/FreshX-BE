using API.DTOs.AccountCus;
using API.Models;

namespace API.Services
{
    public interface IAccountCusService
    {
        Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(CreateAccountCusDto registerDto);
        Task<(bool Succeeded, string Token, int? CustomerId, string ErrorMessage)> LoginAsync(LoginCusDto loginDto);
        // Các phương thức khác nếu cần
        Task LogoutAsync();
        Task<AccountCus> ValidateCredentials(string email, string password);
        Task AddAsync(AccountCus accountCus);
    }

}