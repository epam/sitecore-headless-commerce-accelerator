using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.WishList
{
    public class UpdateWishListTests : BaseWishListTest
    {
        public UpdateWishListTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "")]
        public void T1_PUTWishListLine_ValidProduct_VerifyEndpointIsNotImplemented()
        {
            // Arrange, Act
            var result = HcaService.AddWishListLine(AddingProduct);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { VerifyNotImplemented(result); });
        }
    }
}