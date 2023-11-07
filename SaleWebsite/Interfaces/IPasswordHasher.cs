namespace SaleWebsite.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string passwordInput);

        bool Verify(string passwordInput, string passwordHash);
    }
}
