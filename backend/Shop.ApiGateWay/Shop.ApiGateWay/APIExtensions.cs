using System.Text.Json;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shop.ApiGateWay.Models;

namespace Shop.ApiGateWay;

public static class APIExtensions
{
    public static string GetForwardingRoute(this IConfiguration _configuration, string name) =>
        _configuration.GetSection("Routes").Get<List<RoutesInfo>>().FirstOrDefault(r => r.Name == name).Downstream;
    public static void AddAPIAutentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme,
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["tasty-cookie"];
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddAuthorization(o =>
        {
            o.AddPolicy("Admin", policy => policy.RequireClaim("isAdmin", "True"));
        });
    }
}