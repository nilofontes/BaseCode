namespace BaseCode.Models
{
    public class LoginRequest : BaseLogin
    {
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public string ApiKey { get; set;}
    }
}
