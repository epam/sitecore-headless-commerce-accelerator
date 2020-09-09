using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.WishList
{
    public class RemoveWishListLineTests : BaseWishListTest
    {
        public RemoveWishListLineTests(HcaUserRole userRole) : base(userRole)
        {
        }

        [Test(Description = "")]
        public void T1_DELETEWishListLine_ValidProduct_VerifyEndpointIsNotImplemented()
        {
            // Arrange, Act
            var result = ApiContext.WishList.RemoveWishListLine(AddingProduct.VariantId);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { VerifyNotImplemented(result); });
        }
    }
}