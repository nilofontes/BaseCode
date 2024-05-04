using BaseCode.Interface;
using BaseCode.Models;
using BaseCode.Utils;

namespace BaseCode.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILoginRequest _loginrequest;
       
        public AuthService(ILoginRequest loginrequest) 
        { 
           
            _loginrequest = loginrequest;
            
        }

        public TokenResponse AuthenticateASync (string username, string password, string apikey)
        {
           
            LoginRequest dbResponse = _loginrequest.GetUserByPassword(username, password);
            if (dbResponse != null && dbResponse.ApiKey == apikey)
            {
                var acessToken = JwtUtils.GenerateJwtToken(username, apikey, 1);

                var refreshToken = JwtUtils.GenerateJwtToken(username, apikey, 24);

                return new TokenResponse{ Token = acessToken, RefreshToken = refreshToken, StatusCode = 200};
            }
            else
            {
                return new TokenResponse { StatusCode = 400, ErrorMessage = "Credenciais não foram informadas corretamente."};

            }          
        }

        public TokenResponse ValidateJwt(HttpContext httpContext)
        {
            try
            {
                // Verifica se o cabeçalho de autorização está presente na solicitação
                if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new TokenResponse { ErrorMessage = "Cabeçalho de autorização ausente."};
                }

                // Obtém o token JWT do cabeçalho de autorização
                string authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    return new TokenResponse { ErrorMessage = "Token JWT não fornecido." };
                }

                // Verifica se o cabeçalho de autorização usa o esquema Bearer
                if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return new TokenResponse { ErrorMessage = "Esquema de autenticação inválido. Use o esquema Bearer." };
                }

                // Extrai o token JWT do cabeçalho de autorização
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();

                // Verifica se o token JWT está vazio
                if (string.IsNullOrEmpty(token))
                {
                    return new TokenResponse { ErrorMessage = "Token JWT vazio." };
                }              

                // Retorna o resultado da validação do token
                return new TokenResponse {Token = token, StatusCode =200};
            }
            catch (Exception ex)
            {

                return new TokenResponse { StatusCode = 500, ErrorMessage = "Ocorreu um erro ao validar o token JWT." + ex.Message };
            }
        }

        public TokenResponse AuthenticateRefreshToken (string currentRefreshToken)
        {
            
            LoginRequest dbResponse = _loginrequest.GetUserbyRefreshToken(currentRefreshToken);
            if (dbResponse != null)
            {
                var acessToken = JwtUtils.GenerateJwtToken(dbResponse.UserName, dbResponse.ApiKey, 1);

                var refreshToken = JwtUtils.GenerateJwtToken(dbResponse.UserName, dbResponse.ApiKey, 24);

                _loginrequest.UpdateRefreshToken(dbResponse, refreshToken);

                return new TokenResponse { Token = acessToken, RefreshToken = refreshToken, StatusCode = 200 };
            }
            else
            {
                return new TokenResponse { StatusCode = 400, ErrorMessage = "RefreshToken inválido." };
            }
            
        }

        
    }
}
