using API.DTOs.AccountEmp;
using API.Server.DTOs.Account;
using API.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEmpController : ControllerBase
    {
        private readonly IAccountEmpService _accountService;

        public AccountEmpController(IAccountEmpService accountService)
        {
            _accountService = accountService;
        }

        // API endpoint cho đăng ký người dùng mới
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterEmpDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errorMessage) = await _accountService.RegisterAsync(registerDto);
            if (!succeeded)
                return BadRequest(errorMessage);

            return Ok("User registered successfully.");
        }

        // API endpoint cho đăng nhập người dùng
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginEmpDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, token, errorMessage) = await _accountService.LoginAsync(loginDto);
            if (succeeded)
                return Ok(new { Token = token });

            return BadRequest(errorMessage);
        }

        // API endpoint cho đăng xuất người dùng
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok("Logout successful.");
        }

        // API endpoint cho quên mật khẩu
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errorMessage) = await _accountService.ForgotPasswordAsync(forgotPasswordDto);
            if (!succeeded)
                return BadRequest(errorMessage);

            return Ok("Password reset instructions sent to your email.");
        }

        // API endpoint cho đặt lại mật khẩu
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errorMessage) = await _accountService.ResetPasswordAsync(resetPasswordDto);
            if (!succeeded)
                return BadRequest(errorMessage);

            return Ok("Password reset successful.");
        }
    }
}
