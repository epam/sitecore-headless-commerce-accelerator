namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Account
{
    public class CreateAccountRequest
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public CreateAccountRequest() { }

        public CreateAccountRequest(string email, string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password= password;
        }
    }
}
