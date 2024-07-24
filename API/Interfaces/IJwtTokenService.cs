using System;
using System.IdentityModel.Tokens.Jwt; // Sử dụng cho việc tạo JWT
using System.Security.Claims; // Sử dụng cho Claims trong JWT
using System.Text; // Sử dụng cho Encoding
using Microsoft.IdentityModel.Tokens; // Sử dụng cho Token Validation Parameters

namespace API.Services
{
    // Interface định nghĩa các phương thức cho dịch vụ JWT
    public interface IJwtTokenService
    {
        string GenerateToken(string username); // Phương thức tạo JWT token
    }
}
