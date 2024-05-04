namespace BaseCode.Models
{
    public class LoginRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public string ApiKey { get; set;}
    }
}
