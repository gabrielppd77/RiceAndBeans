using Application.Common.Interfaces.PasswordHash;
using System.Security.Cryptography;

namespace Infrastructure.PasswordHash
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        private readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        public string HashPassword(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                var hash = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithm).GetBytes(KeySize);

                var hashBytes = new byte[SaltSize + KeySize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var hash = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithm).GetBytes(KeySize);

            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
