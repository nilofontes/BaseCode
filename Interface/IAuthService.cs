using BaseCode.Models;

namespace BaseCode.Interface
{
    public interface IAuthService
    {
        TokenResponse AuthenticateASync(string username, string password, string apiKey);
        TokenResponse ValidateJwt(HttpContext context);
        TokenResponse AuthenticateRefreshToken(string currentRefreshToken);

    }
}
