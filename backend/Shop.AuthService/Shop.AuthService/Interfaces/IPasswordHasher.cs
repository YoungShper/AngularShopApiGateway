namespace Shop.AuthService.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    public bool VerifyHashedPassword(string password, string hashedPassword);
    
}