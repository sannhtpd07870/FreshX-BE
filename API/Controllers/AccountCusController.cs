using API.DTOs.AccountCus;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCusController : ControllerBase
    {
        private readonly IAccountCusService _accountCusService;

        public AccountCusController(IAccountCusService accountCusService)
        {
            _accountCusService = accountCusService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountCusDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errorMessage) = await _accountCusService.RegisterAsync(registerDto);

            if (!succeeded)
                return BadRequest(errorMessage);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCusDto loginDto)
        {
            var (succeeded, token, customerId, errorMessage) = await _accountCusService.LoginAsync(loginDto);

            if (!succeeded)
            {
                return BadRequest(new { errorMessage });
            }

            return Ok(new { token, customerId });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountCusService.LogoutAsync();
            return Ok("Logout successful.");
        }
    }

}