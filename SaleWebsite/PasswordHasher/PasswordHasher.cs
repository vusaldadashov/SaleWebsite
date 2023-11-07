using SaleWebsite.Interfaces;
using System.Security.Cryptography;

namespace SaleWebsite.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private const int SaltSize = 256 / 8;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';

        public string Hash(string passwordInput)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(passwordInput, salt, Iterations, _hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(salt) , Convert.ToBase64String(hash));

            throw new NotImplementedException();
        }

        public bool Verify(string passwordInput, string passwordHash)
        {
            var elements = passwordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);


            var passwordInputHash = Rfc2898DeriveBytes.Pbkdf2(passwordInput, salt, Iterations, _hashAlgorithmName, KeySize);

            return CryptographicOperations.FixedTimeEquals(passwordInputHash, hash);
            throw new NotImplementedException();
        }
    }
}
