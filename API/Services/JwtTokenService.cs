using System;
using System.IdentityModel.Tokens.Jwt; // Sử dụng cho việc tạo JWT
using System.Security.Claims; // Sử dụng cho Claims trong JWT
using System.Text; // Sử dụng cho Encoding
using API.Server.Models;
using Microsoft.Extensions.Configuration; // Sử dụng để truy cập cấu hình
using Microsoft.IdentityModel.Tokens; // Sử dụng cho Token Validation Parameters

namespace API.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        // Khởi tạo JwtTokenService với cấu hình từ appsettings.json
        public JwtTokenService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }

        // Tạo token JWT với thông tin người dùng
        public string GenerateToken(AccountEmp accountEmp)
        {
            // Tạo các claims cho token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountEmp.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("EmployeeId", accountEmp.EmployeeId.ToString()),
                new Claim("RoleId", accountEmp.RoleId.ToString()),
                new Claim("Email", accountEmp.Email)
            };

            // Tạo khóa ký
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo token
            var response = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            // Trả về token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(response);
        }
    }
}
