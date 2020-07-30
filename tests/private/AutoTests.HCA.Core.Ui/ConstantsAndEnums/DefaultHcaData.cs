using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums
{
    public class DefaultHcaData
    {
        public DefaultHcaData(int productId, UserLogin userLogin)
        {
            ProductId = productId;
            UserLogin = userLogin;
        }

        public int ProductId { get; set; }

        public UserLogin UserLogin { get; set; }
    }
}