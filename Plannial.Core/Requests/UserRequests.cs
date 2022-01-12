namespace Plannial.Core.Requests
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string Email, string Password);
}
