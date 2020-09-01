using System;
using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.Helpers;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.BaseTests;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.Account
{
    [Parallelizable(ParallelScope.None)]
    [ApiTest]
    public class BaseAccountTest : BaseHcaApiTest
    {
        protected static readonly UserLogin DefUser = TestsData.GetUser().Credentials;

        protected IHcaApiService HcaService;
        protected UserManagerHelper UserManager;
        protected readonly IEnumerable<Address> AddressesCollection;

        public const string AUTHORIZATION_COOKIE_NAME = ".AspNet.Cookies";

        public BaseAccountTest()
        {
            AddressesCollection = new List<Address>
            {
                new Address
                {
                    FirstName = StringHelpers.RandomString(10),
                    LastName = StringHelpers.RandomString(10),
                    Address1 = StringHelpers.GetRandomAddressString(),
                    Address2 = StringHelpers.GetRandomAddressString(),
                    City = StringHelpers.RandomString(10),
                    Country = "United States",
                    Email = GetRandomEmail(),
                    CountryCode = "US",
                    State = "AL",
                    ZipPostalCode = "2335",
                    IsPrimary = false,
                },
                new Address
                {
                    FirstName = StringHelpers.RandomString(10),
                    LastName = StringHelpers.RandomString(10),
                    Address1 = StringHelpers.GetRandomAddressString(),
                    Address2 = StringHelpers.GetRandomAddressString(),
                    City = StringHelpers.RandomString(10),
                    Country = "United States",
                    Email = GetRandomEmail(),
                    CountryCode = "US",
                    State = "AL",
                    ZipPostalCode = "2335",
                    IsPrimary = true,
                }
            };
        }

        [SetUp]
        public virtual void SetUp()
        {
            HcaService = TestsHelper.CreateHcaApiClient();
        }

        protected static string GetRandomEmail()
        {
            return $"{StringHelpers.RandomString(10)}@autotests.com";
        }

        protected void VerifyAddressResponse(IEnumerable<Address> expectedAddresses, IEnumerable<Address> actualAddresses,
            bool isNewAddress = false)
        {
            // Data
            Assert.NotNull(actualAddresses, $"The response should contain information about user addresses.");

            if (expectedAddresses.Any())
            {
                if (actualAddresses.Any())
                {
                    Assert.AreEqual(expectedAddresses.Count(), actualAddresses.Count(),
                        "The expected addresses number doesn't match the actual addresses number.");

                    foreach (var address in actualAddresses)
                    {
                        // Data -> Address 
                        ExtendedAssert.NotNull(address, nameof(address));

                        Address expAddress;

                        if (isNewAddress)
                        {
                            expAddress = expectedAddresses.FirstOrDefault(x => x.Email == address.Email) ??
                                             throw new Exception($"the list of addresses must not contain email='{address.Email}'");
                            ExtendedAssert.NotNullOrWhiteSpace(address.Email, nameof(address.Email));
                            ExtendedAssert.NotNullOrWhiteSpace(address.ExternalId, nameof(address.ExternalId));
                            ExtendedAssert.NotNullOrWhiteSpace(address.Name, nameof(address.Name));
                        }
                        else
                        {
                            expAddress = expectedAddresses.FirstOrDefault(x => x.ExternalId == address.ExternalId) ??
                                         throw new Exception($"the list of addresses must not contain externalId='{address.ExternalId }'");
                            ExtendedAssert.AreEqual(expAddress.ExternalId, address.ExternalId, nameof(address.ExternalId));
                            ExtendedAssert.AreEqual(expAddress.Name, address.Name, nameof(address.Name));
                        }

                        ExtendedAssert.AreEqual(expAddress.FirstName, address.FirstName, nameof(address.FirstName));
                        ExtendedAssert.AreEqual(expAddress.LastName, address.LastName, nameof(address.LastName));
                        ExtendedAssert.AreEqual(expAddress.Address1, address.Address1, nameof(address.Address1));
                        ExtendedAssert.AreEqual(expAddress.Address2, address.Address2, nameof(address.Address2));
                        ExtendedAssert.AreEqual(expAddress.Country, address.Country, nameof(address.Country));
                        ExtendedAssert.AreEqual(expAddress.CountryCode, address.CountryCode, nameof(address.CountryCode));
                        ExtendedAssert.AreEqual(expAddress.City, address.City, nameof(address.City));
                        ExtendedAssert.AreEqual(expAddress.ZipPostalCode, address.ZipPostalCode, nameof(address.ZipPostalCode));
                        ExtendedAssert.AreEqual(expAddress.State, address.State, nameof(address.State));
                        ExtendedAssert.AreEqual(expAddress.IsPrimary, address.IsPrimary, nameof(address.IsPrimary));
                    }
                }
                else
                {
                    Assert.Fail($"{nameof(actualAddresses)} can't be empty.");
                }
            }
            else
            {
                ExtendedAssert.Empty(actualAddresses, nameof(actualAddresses));
            }

        }
    }
}
