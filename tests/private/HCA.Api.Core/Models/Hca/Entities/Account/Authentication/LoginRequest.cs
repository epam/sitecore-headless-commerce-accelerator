namespace HCA.Api.Core.Models.Hca.Entities.Account.Authentication
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public LoginRequest() { }

        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
