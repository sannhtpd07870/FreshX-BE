using API.DTOs.AccountEmp;
using API.Server.DTOs.Account;
using API.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Server.Interfaces
{
    public interface IAccountEmpService
    {
        Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(RegisterEmpDto registerDto);
        // Phương thức đăng nhập và trả về token JWT cùng với thông tin người dùng nếu thành công
        Task<(bool Succeeded, object User, string ErrorMessage)> LoginAsync(LoginEmpDto loginDto);

        Task LogoutAsync();
        Task<(bool Succeeded, string ErrorMessage)> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<(bool Succeeded, string ErrorMessage)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<AccountEmp> ValidateCredentials(string email, string password);
        Task AddAsync(AccountEmp accountEmp);

        // Phương thức GET tất cả người dùng
        Task<IEnumerable<AccountEmp>> GetAllAsync();

        // Phương thức GET người dùng theo ID
        Task<AccountEmp> GetByIdAsync(int id);
    }
}
