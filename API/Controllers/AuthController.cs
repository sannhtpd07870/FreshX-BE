using Microsoft.AspNetCore.Mvc; // Sử dụng cho ControllerBase
using API.Models; // Namespace chứa các DTOs
using API.Services; // Namespace chứa các dịch vụ
using AutoMapper;
using API.Server.DTOs.Account;
using API.DTOs.Account;
using API.Server.Models;
using API.Server.Interfaces; // Sử dụng cho AutoMapper

namespace API.Controllers
{
    [Route("api/[controller]")] // Định tuyến cho API
    [ApiController] // Đánh dấu lớp là API Controller
    public class AuthController : ControllerBase
    {
        private readonly IAccountEmpService _accountEmpService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthController(IAccountEmpService accountEmpService, IJwtTokenService jwtTokenService, IMapper mapper)
        {
            _accountEmpService = accountEmpService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Xác thực thông tin đăng nhập
            var accountEmp = await _accountEmpService.ValidateCredentials(loginDto.UserName, loginDto.Password);
            if (accountEmp == null)
            {
                return Unauthorized(); // Trả về 401 nếu thông tin đăng nhập không hợp lệ
            }

            // Tạo JWT token
            var token = _jwtTokenService.GenerateToken(accountEmp.Username);
            return Ok(new { Token = token }); // Trả về token cho client
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateAccountEmpDto createAccountEmpDto)
        {
            // Chuyển đổi DTO sang model
            var accountEmp = _mapper.Map<AccountEmp>(createAccountEmpDto);
            await _accountEmpService.AddAsync(accountEmp); // Thêm tài khoản mới
            return Ok(); // Trả về kết quả thành công
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Thực hiện các thao tác cần thiết để đăng xuất người dùng (thường là phía client)
            return Ok(); // Trả về kết quả thành công
        }
    }
}
