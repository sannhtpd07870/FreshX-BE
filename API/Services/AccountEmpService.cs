using API.Server.DTOs.Account;
using API.Server.Interfaces;
using API.Server.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Server.Services
{
    public class AccountEmpervice : IAccountEmpService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context;

        public AccountEmpervice(IJwtTokenService jwtTokenService, ApplicationDbContext context)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        // Phương thức để đăng ký người dùng mới
        public async Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(RegisterDto registerDto)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            if (await _context.AccountEmp.AnyAsync(u => u.Username == registerDto.Email))
            {
                return (false, "User already exists.");
            }

            // Tạo người dùng mới
            var user = new AccountEmp
            {
                Username = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), // Mã hóa mật khẩu
                Email = registerDto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = 1 // Đặt RoleId mặc định
            };

            // Lưu người dùng vào cơ sở dữ liệu
            _context.AccountEmp.Add(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        // Phương thức để đăng nhập và tạo token JWT
        public async Task<(bool Succeeded, string Token, string ErrorMessage)> LoginAsync(LoginDto loginDto)
        {
            // Tìm người dùng theo tên đăng nhập
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Username == loginDto.UserName);

            // Kiểm tra thông tin đăng nhập
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) // Kiểm tra mật khẩu đã mã hóa
            {
                return (false, null, "Invalid login attempt.");
            }

            // Tạo token JWT
            var token = _jwtTokenService.GenerateToken(user.Username);
            return (true, token, null);
        }

        // Phương thức để đăng xuất người dùng
        public Task LogoutAsync()
        {
            // Nếu bạn muốn thực hiện logic đăng xuất, hãy thêm vào đây
            return Task.CompletedTask;
        }

        // Phương thức xác thực thông tin đăng nhập
        public async Task<AccountEmp> ValidateCredentials(string username, string password)
        {
            var user = await _context.AccountEmp.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }

        // Phương thức thêm tài khoản mới
        public async Task AddAsync(AccountEmp accountEmp)
        {
            _context.AccountEmp.Add(accountEmp);
            await _context.SaveChangesAsync();
        }

        // Phương thức GET tất cả người dùng
        public async Task<IEnumerable<AccountEmp>> GetAllAsync()
        {
            return await _context.AccountEmp.ToListAsync();
        }

        // Phương thức GET người dùng theo ID
        public async Task<AccountEmp> GetByIdAsync(int id)
        {
            return await _context.AccountEmp.FindAsync(id);
        }
    }
}
