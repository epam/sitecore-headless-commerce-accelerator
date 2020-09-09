using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.WishList
{
    public class AddWishListLineTests : BaseWishListTest
    {
        public AddWishListLineTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "")]
        public void T1_POSTWishListLine_ValidProduct_VerifyEndpointIsNotImplemented()
        {
            // Arrange, Act
            var result = ApiContext.WishList.AddWishListLine(AddingProduct);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { VerifyNotImplemented(result); });
        }
    }
}