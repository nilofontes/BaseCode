namespace BaseCode.Models
{
    public class TokenResponse 
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

    }
}
