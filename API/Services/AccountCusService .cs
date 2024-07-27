using API.DTOs.AccountCus;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountCusService : IAccountCusService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountCusService(IJwtTokenService jwtTokenService, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<(bool Succeeded, string ErrorMessage)> RegisterAsync(CreateAccountCusDto registerDto)
        {
            if (await _context.AccountCus.AnyAsync(u => u.Email == registerDto.Email))
            {
                return (false, "User already exists.");
            }

            var user = _mapper.Map<AccountCus>(registerDto);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            _context.AccountCus.Add(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Succeeded, string Token, int? CustomerId, string ErrorMessage)> LoginAsync(LoginCusDto loginDto)
        {
            var user = await _context.AccountCus.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            // Kiểm tra thông tin đăng nhập
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return (false, null, null, "Invalid login attempt.");
            }

            var token = _jwtTokenService.GenerateToken(user);
            return (true, token, user.CustomerId, null);
        }

        public Task LogoutAsync()
        {
            // Logic for logout if necessary
            return Task.CompletedTask;
        }

        public async Task<AccountCus> ValidateCredentials(string email, string password)
        {
            var user = await _context.AccountCus.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public async Task AddAsync(AccountCus accountCus)
        {
            _context.AccountCus.Add(accountCus);
            await _context.SaveChangesAsync();
        }
    }
}