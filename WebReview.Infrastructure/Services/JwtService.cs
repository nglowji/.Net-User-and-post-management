using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebReview.Application.Interfaces;
using WebReview.Domain.Constants;
using WebReview.Domain.Entities;

namespace WebReview.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly string _key;

    public JwtService(IConfiguration configuration)
    {
        _key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Thiếu cấu hình khóa JWT.");
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, string.IsNullOrWhiteSpace(user.Role) ? UserRoles.User : user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "WebReview.API",
            audience: "WebReview.API",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}