namespace Application.Auth.Shared.Abstractions.Interfaces
{
    public interface IOtpService
    {
        Task<string> GenerateToken();
        Task<bool> CheckDateTimeForLogin(string phoneNumber);
    }
}
