namespace Contracts.Services.Authentication
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}