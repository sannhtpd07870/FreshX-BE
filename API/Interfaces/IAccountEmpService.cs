using API.DTOs.AccountEmp;
using API.Server.DTOs.Account;
using API.Server.Models;

namespace API.Server.Interfaces
{
    public interface IAccountEmpService
    {
        Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(RegisterEmpDto registerDto);
        Task<(bool Succeeded, string Token, string ErrorMessage)> LoginAsync(LoginEmpDto loginDto);
        Task LogoutAsync();
        Task<(bool Succeeded, string ErrorMessage)> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<(bool Succeeded, string ErrorMessage)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<AccountEmp> ValidateCredentials(string email, string password);
        Task AddAsync(AccountEmp accountEmp);
    }
}
