using API.DTOs.AccountEmp;
using API.Models;
using API.Server.DTOs.Account;
using API.Server.Interfaces;
using API.Server.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Server.Services
{
    public class AccountEmpService : IAccountEmpService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context;

        // Constructor để khởi tạo các dịch vụ cần thiết
        public AccountEmpService(IJwtTokenService jwtTokenService, ApplicationDbContext context)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        // Phương thức đăng ký tài khoản mới
        public async Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(RegisterEmpDto registerDto)
        {
            // Kiểm tra xem email đã tồn tại hay chưa
            if (await _context.AccountEmp.AnyAsync(u => u.Email == registerDto.Email))
            {
                return (false, "User already exists.");
            }

            // Tạo đối tượng Employee mới với các giá trị mặc định không null
            var employee = new Employee
            {
                Name = string.Empty, // Giá trị mặc định không null
                BirthDate = DateTime.MinValue, // Giá trị mặc định không null
                Gender = string.Empty, // Giá trị mặc định không null
                Address = string.Empty, // Giá trị mặc định không null
                Phone = string.Empty, // Giá trị mặc định không null
                Email = string.Empty, // Giá trị mặc định không null
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            };

            // Thêm Employee vào cơ sở dữ liệu và lưu để lấy Id
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            // Tạo đối tượng AccountEmp mới và gán EmployeeId từ Employee mới tạo
            var user = new AccountEmp
            {
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), // Mã hóa mật khẩu
                Email = registerDto.Email,
                EmployeeId = employee.Id, // Lấy Id của Employee mới tạo
                RoleId = 1, // Đặt giá trị mặc định cho RoleId
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Employee = employee // Gán Employee cho AccountEmp
            };

            _context.AccountEmp.Add(user);
            await _context.SaveChangesAsync();

            return (true, null); // Trả về kết quả thành công
        }

        // Phương thức đăng nhập và tạo token JWT
        public async Task<(bool Succeeded, object User, string ErrorMessage)> LoginAsync(LoginEmpDto loginDto)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            // Kiểm tra thông tin đăng nhập
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return (false, null, "Invalid login attempt.");
            }

            // Tạo token JWT
            var token = _jwtTokenService.GenerateToken(user);

            // Tạo object chứa token và thông tin người dùng
            var userInfo = new
            {
                user = new
                {

                    user.Id,
                    user.Email,
                    user.EmployeeId,
                    user.RoleId,
                },
                Token = token
            };

            return (true, userInfo, null); // Trả về token và thông tin người dùng nếu đăng nhập thành công
        }

        // Phương thức đăng xuất
        public Task LogoutAsync()
        {
            // Logic cho đăng xuất nếu cần thiết
            return Task.CompletedTask;
        }

        // Phương thức quên mật khẩu
        public async Task<(bool Succeeded, string ErrorMessage)> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                return (false, "Email not found.");
            }

            // Logic để gửi email/tạo token đặt lại mật khẩu
            // Bạn có thể sử dụng dịch vụ email để gửi link/token đặt lại mật khẩu tới email của người dùng

            return (true, null); // Trả về kết quả thành công
        }

        // Phương thức đặt lại mật khẩu
        public async Task<(bool Succeeded, string ErrorMessage)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return (false, "Email not found.");
            }

            // Đặt lại mật khẩu
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            _context.AccountEmp.Update(user);
            await _context.SaveChangesAsync();

            return (true, null); // Trả về kết quả thành công
        }

        // Phương thức xác thực thông tin đăng nhập
        public async Task<AccountEmp> ValidateCredentials(string email, string password)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public async Task AddAsync(AccountEmp accountEmp)
        {
            _context.AccountEmp.Add(accountEmp);
            await _context.SaveChangesAsync();
        }
    }
}
