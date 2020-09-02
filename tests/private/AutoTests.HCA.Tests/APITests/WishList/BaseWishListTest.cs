using System.Net;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Helpers;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.WishList;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.WishList
{
    [TestFixture(HcaUserRole.Guest, Description = "")]
    [TestFixture(HcaUserRole.User, Description = "")]
    [ApiTest]
    [Parallelizable(ParallelScope.None)]
    public class BaseWishListTest : BaseHcaApiTest
    {
        [SetUp]
        public void SetUp()
        {
            User = TestsData.GetUser(_userRole);
            HcaService = TestsHelper.CreateHcaApiClient();
            UserManager = TestsHelper.CreateUserManagerHelper(User, HcaService);
        }

        public const string EXP_ERROR_MESSAGE = "The method or operation is not implemented.";

        protected static readonly ProductTestsDataSettings Product = TestsData.GetProduct();

        protected static readonly VariantRequest AddingProduct = new VariantRequest
        {
            ProductId = Product.ProductId,
            DisplayName = Product.ProductName,
            VariantId = Product.VariantId,
            Description = "Description"
        };

        private readonly HcaUserRole _userRole;

        protected IHcaApiService HcaService;
        protected UserManagerHelper UserManager;
        protected HcaUserTestsDataSettings User;

        public BaseWishListTest(HcaUserRole userRole)
        {
            _userRole = userRole;
        }

        protected void VerifyNotImplemented<T>(HcaResponse<T> response)
            where T : class
        {
            ExtendedAssert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode,
                nameof(response.StatusCode));
            var hcaResult = response.Errors as HcaResult;
            ExtendedAssert.NotNull(hcaResult, "Model data");
            ExtendedAssert.AreEqual(HcaStatus.Error, response.Errors.Status, nameof(hcaResult.Status));
            ExtendedAssert.AreEqual(EXP_ERROR_MESSAGE, response.Errors.Error, nameof(response.Errors.Error));
            ExtendedAssert.AreEqual(EXP_ERROR_MESSAGE, response.Errors.ExceptionMessage,
                nameof(response.Errors.ExceptionMessage));
        }
    }
}