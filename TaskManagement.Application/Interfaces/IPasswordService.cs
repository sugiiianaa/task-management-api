namespace TaskManagement.Application.Interfaces
{
    public interface IPasswordService
    {
        byte[] GenerateSalt();
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string storedHashedPassword);
    }
}
