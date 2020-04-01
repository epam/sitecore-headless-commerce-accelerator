//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace Wooli.Foundation.Commerce.Tests.ModelMappers
{
    using System.Collections.Generic;

    using Commerce.ModelMappers;
    using Commerce.Repositories;
    using Commerce.Services.Catalog;

    using Models.Checkout;

    using NSubstitute;

    using Providers;

    using Sitecore.Commerce.Entities.Shipping;

    using Xunit;

    public class AddressPartyMapperTests
    {
        public AddressPartyMapperTests()
        {
            this.catalogService = Substitute.For<ICatalogService>();
            this.currencyProvider = Substitute.For<ICurrencyProvider>();
        }

        private readonly ICatalogService catalogService;
        private readonly ICurrencyProvider currencyProvider;

        [Fact]
        public void MapToParty_ValidInputObject_ObjectIsMappedCorrectly()
        {
            // Setup
            var input = new AddressModel
            {
                PartyId = "partyId",
                IsPrimary = true
            };

            // Execute
            var addressPartyMapper = new EntityMapper(this.catalogService, this.currencyProvider);
            var result = addressPartyMapper.MapToParty(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("partyId", result.PartyId);
            Assert.True(result.IsPrimary);
        }

        [Fact]
        public void MapToShippingInfoArgument_ValidInputObject_ObjectIsMappedCorrectly()
        {
            // Setup
            var input = new ShippingMethodModel
            {
                PartyId = "partyId",
                LineIds = null,
                ShippingPreferenceType = ShippingOptionType.ElectronicDelivery.Value.ToString()
            };

            // Execute
            var addressPartyMapper = new EntityMapper(this.catalogService, this.currencyProvider);
            var result = addressPartyMapper.MapToShippingInfoArgument(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("partyId", result.PartyId);
            Assert.Empty(result.LineIds);
            Assert.Equal(ShippingOptionType.ElectronicDelivery.Value, result.ShippingPreferenceType.Value);
            Assert.Equal(ShippingOptionType.ElectronicDelivery.Name, result.ShippingPreferenceType.Name);
        }

        [Fact]
        public void MapToShippingInfoArgument_ValidInputObjectWithLineIdsNotEmpty_ObjectIsMappedCorrectly()
        {
            // Setup
            var input = new ShippingMethodModel
            {
                LineIds = new List<string>
                {
                    "1",
                    "2"
                }
            };

            // Execute
            var addressPartyMapper = new EntityMapper(this.catalogService, this.currencyProvider);
            var result = addressPartyMapper.MapToShippingInfoArgument(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.LineIds.Count);
        }
    }
}