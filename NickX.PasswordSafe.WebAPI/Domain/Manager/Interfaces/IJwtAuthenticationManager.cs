namespace NickX.PasswordSafe.WebAPI.Domain.Manager.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
