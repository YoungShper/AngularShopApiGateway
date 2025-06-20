using Microsoft.AspNetCore.Components;

namespace Shop.AuthService.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsAdmin { get; set; }
        public string? City { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public string ClassName { get; set; } = "users";
    }
}
