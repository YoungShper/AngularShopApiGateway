using Shop.AuthService.Models;

namespace Shop.AuthService.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(UserModel user);
}