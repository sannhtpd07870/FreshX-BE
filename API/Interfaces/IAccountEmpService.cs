using API.Server.DTOs.Account;
using API.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Server.Interfaces
{
    // Định nghĩa interface cho dịch vụ tài khoản
    public interface IAccountEmpService
    {
        // Phương thức để đăng ký tài khoản mới
        Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(RegisterDto registerDto);

        // Phương thức để đăng nhập và trả về token JWT nếu thành công
        Task<(bool Succeeded, string Token, string ErrorMessage)> LoginAsync(LoginDto loginDto);

        // Phương thức để đăng xuất
        Task LogoutAsync();

        // Phương thức xác thực thông tin đăng nhập
        Task<AccountEmp> ValidateCredentials(string username, string password);

        // Phương thức thêm tài khoản mới
        Task AddAsync(AccountEmp accountEmp);

        // Phương thức GET tất cả người dùng
        Task<IEnumerable<AccountEmp>> GetAllAsync();

        // Phương thức GET người dùng theo ID
        Task<AccountEmp> GetByIdAsync(int id);
    }
}
