namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Account
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public ChangePasswordRequest() { }

        public ChangePasswordRequest(string email, string oldPassword, string newPassword)
        {
            Email = email;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
