using API.DTOs.AccountCus;
using API.Models;

namespace API.Services
{
    public interface IAccountCusService
    {
        Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(CreateAccountCusDto registerDto);
        Task<(bool Succeeded, string Token, string ErrorMessage)> LoginAsync(LoginCusDto loginDto);
        Task LogoutAsync();
        Task<AccountCus> ValidateCredentials(string email, string password);
        Task AddAsync(AccountCus accountCus);
    }

}