using BaseCode.Models;

namespace BaseCode.Interface
{
    public interface ILoginRequest
    {
        LoginRequest GetUserByPassword(string username, string password);
        LoginRequest GetUserbyRefreshToken(string refreshToken);
        void UpdateRefreshToken(LoginRequest loginrequest, string refreshToken);
    }
}
