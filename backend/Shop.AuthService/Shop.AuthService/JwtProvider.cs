using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop.AuthService.Interfaces;
using Shop.AuthService.Interfaces.Auth;
using Shop.AuthService.Models;

namespace Shop.AuthService;

public class JwtProvider : IJwtProvider
{
    private string Key;
    private string ExpiresHours;

    public JwtProvider(IConfiguration configuration)
    {
        Key = configuration["Jwt:Key"];
        ExpiresHours = configuration["Jwt:ExpiresHours"];
    }

    public string GenerateToken(UserModel user)
    {
        Claim[] claims = [new ("id", user.Id.ToString()), new("isAdmin", user.IsAdmin.ToString()),/* new("mail", user.Mail)*/];
        
        var signing = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)), SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(expires: DateTime.UtcNow.AddHours(int.Parse(ExpiresHours)), signingCredentials: signing, claims:claims);
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}