using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;

namespace Shop.AuthService.Interfaces.Auth;

public interface IAuth
{
    public Task<string> Login(string mail, string password, CancellationToken cancellationToken = default);
    public Task<bool> Register(string login, string password, CancellationToken cancellationToken = default);
}