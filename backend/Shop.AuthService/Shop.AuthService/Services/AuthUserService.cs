using System.Collections.Specialized;
using Microsoft.AspNetCore.Http.HttpResults;
using Shop.AuthService.Interfaces;
using Shop.AuthService.Interfaces.Auth;
using Shop.AuthService.Models;
using Shop.AuthService.Repository;

namespace Shop.AuthService.Services;

public class AuthUserService : IAuth
{
    private IPasswordHasher _passwordHasher;
    private IDbRepository<UserModel> _userRepository;
    IJwtProvider _jwtProvider;

    public AuthUserService(IPasswordHasher passwordHasher, IDbRepository<UserModel> userRepository, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider; 
    }

    public async Task<string> Login(string login, string password, CancellationToken cancellationToken = default)
    {
        var data = new NameValueCollection();
        data.Add("mail", login);
        
        var dbUser = await _userRepository.GetAllAsync(data, cancellationToken);
        if (dbUser.Any() && _passwordHasher.VerifyHashedPassword(password, dbUser.First().Password))
        {
            return _jwtProvider.GenerateToken(dbUser.First());
        }
        return "";

    }

    public async Task<bool> Register(string mail, string password, CancellationToken cancellationToken = default)
    {
        var hashedPassword = _passwordHasher.HashPassword(password);
        
        var user = new UserModel {Id = Guid.NewGuid(), Mail = mail, Password = hashedPassword, IsAdmin = false};
        
        return await _userRepository.CreateAsync(user, cancellationToken);
    }
}