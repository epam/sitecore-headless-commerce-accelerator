using System;
using System.Collections.Generic;
using System.Text;

namespace UIAutomationFramework.Entities.WebSiteEntities
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
