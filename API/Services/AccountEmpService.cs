using API.DTOs.AccountEmp;
using API.Models;
using API.Server.DTOs.Account;
using API.Server.Interfaces;
using API.Server.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API.Server.Services
{
    public class AccountEmpService : IAccountEmpService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context;

        public AccountEmpService(IJwtTokenService jwtTokenService, ApplicationDbContext context)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        public async Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(RegisterEmpDto registerDto)
        {
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

            var user = new AccountEmp
            {
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Email = registerDto.Email,
                EmployeeId = employee.Id, // Lấy Id của Employee mới tạo
                RoleId = 1, // Đặt giá trị mặc định cho RoleId
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Employee = employee // Gán Employee cho AccountEmp
            };

            _context.AccountEmp.Add(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Succeeded, string Token, string ErrorMessage)> LoginAsync(LoginEmpDto loginDto)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return (false, null, "Invalid login attempt.");
            }

            var token = _jwtTokenService.GenerateToken(user.Email);
            return (true, token, null);
        }

        public Task LogoutAsync()
        {
            // Logic for logout if necessary
            return Task.CompletedTask;
        }

        public async Task<(bool Succeeded, string ErrorMessage)> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                return (false, "Email not found.");
            }

            // Logic to send reset password email/token
            // You can use any email service to send the reset link/token to the user's email

            return (true, null);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return (false, "Email not found.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            _context.AccountEmp.Update(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }

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
