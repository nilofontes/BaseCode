namespace BaseCode.Models
{
    public class LoginRequest
    {
        var teste = "123";
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public string ApiKey { get; set;}
    }
}
