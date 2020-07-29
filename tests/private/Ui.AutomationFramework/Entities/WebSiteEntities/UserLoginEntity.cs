namespace Ui.AutomationFramework.Entities.WebSiteEntities
{
    public class UserLoginEntity
    {
        public string Email { get; }
        public string Password { get; }

        public UserLoginEntity(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
