namespace TaskManagement.Application.Interfaces
{
    public interface IGenerateJwtToken
    {
        public string GenerateJwtTokenSync(string email, string role, string secretKey);
    }
}
