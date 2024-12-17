namespace TaskManagement.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        byte[] GenerateSalt();
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
