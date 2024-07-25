using Microsoft.AspNetCore.Mvc; // Sử dụng cho ControllerBase
using API.Server.DTOs.Account; // Namespace chứa các DTOs
using API.Server.Interfaces; // Namespace chứa các dịch vụ
using API.Server.Models; // Namespace chứa các model
using AutoMapper;
using System.Threading.Tasks;
using API.Services;

namespace API.Server.Controllers
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

        // API endpoint cho đăng nhập người dùng

        [HttpPost("login")]
        // Endpoint cho đăng nhập người dùng
        public async Task<IActionResult> Login(LoginEmpDto loginDto)
        {
            // Xác thực thông tin đăng nhập
            var accountEmp = await _accountEmpService.ValidateCredentials(loginDto.Email, loginDto.Password);
            if (accountEmp == null)
            {
                return Unauthorized(); // Trả về 401 nếu thông tin đăng nhập không hợp lệ
            }

            // Tạo JWT token
            var token = _jwtTokenService.GenerateToken(accountEmp);

            // Trả về thông tin người dùng và token
            var userInfo = new
            {
                accountEmp.Id,
                accountEmp.Email,
                accountEmp.EmployeeId,
                accountEmp.RoleId,
                access_token = token
            };

            return Ok(userInfo); // Trả về thông tin người dùng và token cho client
        }
        // API endpoint cho đăng ký người dùng mới
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterEmpDto registerDto)
        {
            // Chuyển đổi DTO sang model
            var accountEmp = _mapper.Map<AccountEmp>(registerDto);
            var result = await _accountEmpService.RegisterAsync(registerDto);

            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage); // Trả về lỗi nếu đăng ký không thành công
            }

            return Ok("User registered successfully."); // Trả về kết quả thành công
        }

        // API endpoint cho đăng xuất người dùng
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Thực hiện các thao tác cần thiết để đăng xuất người dùng (thường là phía client)
            return Ok("Logout successful."); // Trả về kết quả thành công
        }
    }
}
